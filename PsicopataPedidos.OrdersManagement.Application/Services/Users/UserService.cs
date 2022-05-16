using PsicopataPedidos.OrdersManagement.Application.Contracts.Persistence;
using PsicopataPedidos.OrdersManagement.Application.Contracts.Services;
using PsicopataPedidos.OrdersManagement.Application.Exceptions;
using PsicopataPedidos.OrdersManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Application.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task FundClientWallet(int userId, decimal amount)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
                throw new NotFoundException(nameof(User), userId);

            if (user.IsAdmin)
                throw new ApplicationException($"{nameof(User)} ({userId}) is not a client.");

            if (amount < 1)
                throw new ApplicationException($"Invalid Amount ({amount}).");

            if (user.Wallet == null)
                user.Wallet = 0;

            user.Wallet += amount;

            await _userRepository.UpdateAsync(user);
        }
    }
}
