using PsicopataPedidos.OrdersManagement.Application.Services.ShoppingList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Application.Contracts.Services
{
    public interface IShoppingListService
    {
        Task<ShoppingListItemResponse> AddShoppingListItem(ShoppingListItemRequest shoppingListItemRequest);
        Task RemoveShoppingListItem(int id);
        Task<IReadOnlyCollection<ShoppingListItemResponse>> GetShoppingListItems();
    }
}
