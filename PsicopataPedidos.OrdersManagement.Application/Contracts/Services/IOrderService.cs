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
        Task<IReadOnlyCollection<OrderResponseDto>> GetAllOrders();
        Task<IReadOnlyCollection<OrderResponseDto>> GetOrdersForCurrentUser();
        Task<OrderResponseDto> MakeOrder();
    }
}
