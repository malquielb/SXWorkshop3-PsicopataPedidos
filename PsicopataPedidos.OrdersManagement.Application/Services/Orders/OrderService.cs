using AutoMapper;
using PsicopataPedidos.OrdersManagement.Application.Contracts.Authorization;
using PsicopataPedidos.OrdersManagement.Application.Contracts.Persistence;
using PsicopataPedidos.OrdersManagement.Application.Contracts.Services;
using PsicopataPedidos.OrdersManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Application.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly ILoggedInUserService _loggedInUserService;

        public OrderService(IOrderRepository orderRepository, IShoppingListRepository shoppingListRepository, 
            IBaseRepository<User> userRepository, IMapper mapper, ILoggedInUserService loggedInUserService)
        {
            _orderRepository = orderRepository;
            _shoppingListRepository = shoppingListRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _loggedInUserService = loggedInUserService;
        }

        public async Task<IReadOnlyCollection<OrderResponseDto>> GetAllOrders()
        {
            var orders = await _orderRepository.ListAllAsync();
            var result = new List<OrderResponseDto>();

            foreach (var order in orders)
            {
                var orderResponse = _mapper.Map<OrderResponseDto>(order);
                
                orderResponse.UserName = (await _userRepository.GetByIdAsync(order.UserId)).FirstName;
                orderResponse.UserName += " " + (await _userRepository.GetByIdAsync(order.UserId)).LastName;

                result.Add(orderResponse);
            }

            return result;
        }

        public async Task<IReadOnlyCollection<OrderResponseDto>> GetOrdersForCurrentUser()
        {
            var orders = await _orderRepository.GetOrdersForUser(_loggedInUserService.UserId);
            var result = _mapper.Map<List<OrderResponseDto>>(orders);

            result.ForEach(order => order.UserName = _loggedInUserService.UserName);

            return result;
        }

        public async Task<OrderResponseDto> MakeOrder()
        {
            var shoppingList = await _shoppingListRepository.GetListForUser(_loggedInUserService.UserId);
            var itemsToOrder = shoppingList.Where(item => item.OrderId == null);

            if (itemsToOrder.Count() < 1)
                throw new ApplicationException("Shopping List is empty.");

            var order = new Order();

            order.UserId = _loggedInUserService.UserId;

            foreach (var item in itemsToOrder)
            {
                order.Total += item.Product.Price * item.Quantity;
            }

            order.ShoppingList = itemsToOrder.ToList();

            order.Date = DateTimeOffset.Now;

            var result = await _orderRepository.AddAsync(order);

            foreach (var item in result.ShoppingList)
                item.OrderId = result.Id;

            result = await _orderRepository.UpdateAsync(result);

            return _mapper.Map<OrderResponseDto>(result);
        }
    }
}
