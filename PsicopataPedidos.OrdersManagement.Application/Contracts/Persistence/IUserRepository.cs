using PsicopataPedidos.OrdersManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Application.Contracts.Persistence
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task FundClientWallet(decimal amount);
    }
}
