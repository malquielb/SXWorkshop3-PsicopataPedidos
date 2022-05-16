using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Application.Services.ShoppingList
{
    public class ShoppingListItemRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
