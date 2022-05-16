using PsicopataPedidos.OrdersManagement.Domain.Entities;
using PsicopataPedidos.OrdersManagement.Identity.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Application.Contracts.Identity
{
    public interface IAuthenticationService
    {
        Task Register(UserRegisterDto userRegister);
        Task<string> Login(UserLoginDto userLogin);
    }
}
