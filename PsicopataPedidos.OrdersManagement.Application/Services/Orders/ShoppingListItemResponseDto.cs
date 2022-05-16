﻿using PsicopataPedidos.OrdersManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Application.Services.Orders
{
    public class ShoppingListItemResponseDto
    {
        public ListProductResponseDto Product { get; set; }
        public int Quantity { get; set; }
    }
}
