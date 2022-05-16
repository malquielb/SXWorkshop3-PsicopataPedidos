using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Application.Services.Orders
{
    public class OrderResponseDto
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public DateTimeOffset Date { get; set; }
        public ICollection<ShoppingListItemResponseDto> ShoppingList { get; set; }
        public string UserName { get; set; }
    }
}
