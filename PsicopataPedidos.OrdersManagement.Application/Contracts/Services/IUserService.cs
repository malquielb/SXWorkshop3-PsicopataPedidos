using PsicopataPedidos.OrdersManagement.Application.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Application.Contracts.Services
{
    public interface IUserService
    {
        Task FundClientWallet(int userId, decimal amount);

        Task<List<UserDto>> GetAllUsers();

        Task<UserDto> GetUserProfile();
    }
}
