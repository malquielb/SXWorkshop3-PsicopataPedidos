using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public DateTimeOffset Date { get; set; }
        public ICollection<ShoppingListItem> ShoppingList { get; set; }
        public int UserId { get; set; }
    }
}
