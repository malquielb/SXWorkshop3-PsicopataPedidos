using AutoMapper;
using PsicopataPedidos.OrdersManagement.Application.Contracts.Authorization;
using PsicopataPedidos.OrdersManagement.Application.Contracts.Persistence;
using PsicopataPedidos.OrdersManagement.Application.Contracts.Services;
using PsicopataPedidos.OrdersManagement.Application.Exceptions;
using PsicopataPedidos.OrdersManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Application.Services.ShoppingList
{
    public class ShoppingListService : IShoppingListService
    {
        private readonly IBaseRepository<ShoppingListItem> _listRepository;
        private readonly IBaseRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        private readonly ILoggedInUserService _loggedInUserService;

        public ShoppingListService(IBaseRepository<ShoppingListItem> listRepository, IBaseRepository<Product> productRepository, 
            IMapper mapper, ILoggedInUserService loggedInUserService)
        {
            _listRepository = listRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _loggedInUserService = loggedInUserService;
        }

        public async Task<ShoppingListItemResponse> AddShoppingListItem(ShoppingListItemRequest shoppingListItemRequest)
        {

            var shoppingListItem = _mapper.Map<ShoppingListItem>(shoppingListItemRequest);
            
            var product = await _productRepository.GetByIdAsync(shoppingListItem.ProductId);

            if (product == null)
                new NotFoundException(nameof(Product), shoppingListItem.ProductId);

            shoppingListItem.Product = product;
            shoppingListItem.UserId = _loggedInUserService.UserId;

            var result = await _listRepository.AddAsync(shoppingListItem);

            return _mapper.Map<ShoppingListItemResponse>(result);
        }

        public async Task<IReadOnlyCollection<ShoppingListItemResponse>> GetShoppingListItems()
        {
            var itemList = await _listRepository.ListAllAsync();
            return _mapper.Map<List<ShoppingListItemResponse>>(itemList);
        }

        public async Task RemoveShoppingListItem(int id)
        {
            var shoppingListItem = await _listRepository.GetByIdAsync(id);

            if (shoppingListItem == null)
                throw new NotFoundException(nameof(ShoppingListItem), id);

            await _listRepository.DeleteAsync(shoppingListItem);
        }
    }
}
