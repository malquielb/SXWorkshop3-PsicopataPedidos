using Microsoft.AspNetCore.Mvc;
using PsicopataPedidos.OrdersManagement.Application.Contracts.Services;
using PsicopataPedidos.OrdersManagement.Application.Services.Orders;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PsicopataPedidos.OrdersManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IReadOnlyCollection<OrderResponseDto>>> GetAllOrders()
        {
            var result = await _orderService.GetAllOrders();
            return Ok(result);
        }

        [HttpGet]
        [Route("user")]
        public async Task<ActionResult<IReadOnlyCollection<OrderResponseDto>>> GetOrdersForCurrentUser()
        {
            var result = await _orderService.GetOrdersForCurrentUser();
            return Ok(result);
        }

        [HttpGet]
        [Route("make")]
        public async Task<ActionResult<IReadOnlyCollection<OrderResponseDto>>> MakeOrder()
        {
            var result = await _orderService.MakeOrder();
            return Ok(result);
        }
    }
}
