using Microsoft.Extensions.Configuration;
using PsicopataPedidos.OrdersManagement.Application.Contracts.Identity;
using PsicopataPedidos.OrdersManagement.Application.Contracts.Persistence;
using PsicopataPedidos.OrdersManagement.Domain.Entities;
using PsicopataPedidos.OrdersManagement.Identity.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Identity.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<string> Login(UserLoginDto userLogin)
        {
            var user = await _userRepository.GetByEmailAsync(userLogin.Email);

            if (user == null)
                throw new ApplicationException("User with specified email dot not exists.");

            var passwordIsCorrect = EncryptionService.VerifyPasswordHash(userLogin.Password, 
                user.PasswordHash, user.PasswordSalt);

            if (!passwordIsCorrect)
                throw new ApplicationException("Password is not correct.");

            return TokenService.CreateToken(user, _configuration);
        }

        public async Task Register(UserRegisterDto userRegister)
        {
            var userWithSameEmail = await _userRepository.GetByEmailAsync(userRegister.Email);

            if (userWithSameEmail != null)
                throw new ApplicationException("User with the same email already exists.");

            var encryptedPassword = EncryptionService.EncryptPassword(userRegister.Password);

            var user = new User()
            {
                FirstName = userRegister.FirstName,
                LastName = userRegister.LastName,
                Email = userRegister.Email,
                PasswordHash = encryptedPassword.PasswordHash,
                PasswordSalt = encryptedPassword.PasswordSalt,
                IsAdmin = false,
                Wallet = 0
            };

            await _userRepository.CreateAsync(user);
        }
    }
}
