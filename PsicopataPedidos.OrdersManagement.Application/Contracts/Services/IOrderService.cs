using PsicopataPedidos.OrdersManagement.Application.Services.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Application.Contracts.Services
{
    public interface IOrderService
    {
        Task<List<OrderResponseDto>> GetAllOrders();
        Task<List<OrderResponseDto>> GetOrdersForCurrentUser();
        Task<OrderResponseDto> MakeOrder();
    }
}
