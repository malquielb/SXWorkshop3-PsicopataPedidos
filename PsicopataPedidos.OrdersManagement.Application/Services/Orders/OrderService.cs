﻿using AutoMapper;
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
        private readonly IBaseRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        private readonly ILoggedInUserService _loggedInUserService;

        public OrderService(IOrderRepository orderRepository, IShoppingListRepository shoppingListRepository, 
            IBaseRepository<User> userRepository, IBaseRepository<Product> productRepository, 
            IMapper mapper, ILoggedInUserService loggedInUserService)
        {
            _orderRepository = orderRepository;
            _shoppingListRepository = shoppingListRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _loggedInUserService = loggedInUserService;
        }

        public async Task<List<OrderResponseDto>> GetAllOrders()
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

        public async Task<List<OrderResponseDto>> GetOrdersForCurrentUser()
        {
            var orders = await _orderRepository.GetOrdersForUser(_loggedInUserService.UserId);
            var result = _mapper.Map<List<OrderResponseDto>>(orders);

            result.ForEach(order => order.UserName = _loggedInUserService.UserName);

            return result;
        }

        public async Task<OrderResponseDto> MakeOrder()
        {
            var shoppingList = await _shoppingListRepository.GetListForUser(_loggedInUserService.UserId);
            var itemsToOrder = shoppingList.Where(item => item.OrderId == null).ToList();

            if (itemsToOrder.Count() < 1)
                throw new ApplicationException("Shopping List is empty.");

            var order = new Order();

            order.UserId = _loggedInUserService.UserId;

            foreach (var item in itemsToOrder)
            {
                if (item.Product.Stock >= item.Quantity)
                {
                    order.Total += item.Product.Price * item.Quantity;
                    item.Product.Stock -= item.Quantity;
                    await _productRepository.UpdateAsync(item.Product);
                }
                else
                {
                    throw new ApplicationException($"{item.Product.Name} have not enought stock for your order.");
                }
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
