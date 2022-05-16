using PsicopataPedidos.OrdersManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Application.Contracts.Persistence
{
    public interface IShoppingListRepository : IBaseRepository<ShoppingListItem>
    {
        Task<IReadOnlyCollection<ShoppingListItem>> GetListForUser(int userId);
        Task<ShoppingListItem> GetItemForUser(int itemId, int userId);
    }
}
