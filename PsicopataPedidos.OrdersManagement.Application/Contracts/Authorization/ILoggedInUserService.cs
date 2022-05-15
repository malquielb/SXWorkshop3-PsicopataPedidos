using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Application.Contracts.Authorization
{
    public interface ILoggedInUserService
    {
        public int UserId { get; }
    }
}
