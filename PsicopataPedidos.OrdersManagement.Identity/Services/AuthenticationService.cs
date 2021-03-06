using Microsoft.Extensions.Configuration;
using PsicopataPedidos.OrdersManagement.Application.Contracts.Identity;
using PsicopataPedidos.OrdersManagement.Application.Contracts.Persistence;
using PsicopataPedidos.OrdersManagement.Application.Exceptions;
using PsicopataPedidos.OrdersManagement.Domain.Entities;
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
        private readonly UserLoginDtoValidator _loginValidator;
        private readonly UserRegisterDtoValidator _registerValidator;

        public AuthenticationService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _loginValidator = new UserLoginDtoValidator();
            _registerValidator = new UserRegisterDtoValidator();
        }

        public async Task<string> Login(UserLoginDto userLogin)
        {
            var validationResult = await _loginValidator.ValidateAsync(userLogin);

            if (validationResult.Errors.Any())
                throw new ValidationException(validationResult);

            var user = await _userRepository.GetByEmailAsync(userLogin.Email);

            if (user == null)
                throw new ApplicationException("User with the specified email do not exists.");

            var passwordIsCorrect = EncryptionService.VerifyPasswordHash(userLogin.Password, 
                user.PasswordHash, user.PasswordSalt);

            if (!passwordIsCorrect)
                throw new ApplicationException("Password is not correct.");

            return TokenService.CreateToken(user, _configuration);
        }

        public async Task Register(UserRegisterDto userRegister)
        {
            var validationResult = await _registerValidator.ValidateAsync(userRegister);

            if (validationResult.Errors.Any())
                throw new ValidationException(validationResult);

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
