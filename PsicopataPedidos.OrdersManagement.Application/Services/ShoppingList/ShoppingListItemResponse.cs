using PsicopataPedidos.OrdersManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Application.Services.ShoppingList
{
    public class ShoppingListItemResponse
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public ListProductResponseDto Product { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }
    }
}
