using Microsoft.AspNetCore.Http;
using PsicopataPedidos.OrdersManagement.Application.Contracts.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Identity.Services
{
    public class LoggedInUserService : ILoggedInUserService
    {
        public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = int.Parse(httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value);
            UserName = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name).Value;
        }

        public string UserName { get; }

        public int UserId { get; }
    }
}
