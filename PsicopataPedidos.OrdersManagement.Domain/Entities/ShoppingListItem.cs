﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Domain.Entities
{
    public class ShoppingListItem
    {
        public int Id { get; set; }
        public int ProductInt { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }
        public int? OrderId { get; set;}
    }
}
