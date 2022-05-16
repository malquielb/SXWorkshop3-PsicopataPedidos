﻿using Microsoft.EntityFrameworkCore;
using PsicopataPedidos.OrdersManagement.Application.Contracts.Persistence;
using PsicopataPedidos.OrdersManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Persistence.Repositories
{
    public class ShoppingListRepository : BaseRepository<ShoppingListItem>, IShoppingListRepository
    {
        public ShoppingListRepository(PsicopataPedidosDbContext context) : base(context)
        {
        }

        public async Task<ShoppingListItem> GetItemForUser(int itemId, int userId)
        {
            return await _context.ShoppingListItems.Where(i => i.UserId == userId)
                    .FirstOrDefaultAsync(i => i.Id == itemId);
        }

        public async Task<IReadOnlyCollection<ShoppingListItem>> GetListForUser(int userId)
        {
            return await _context.ShoppingListItems.Where(i => i.UserId == userId)
                    .ToListAsync();
        }
    }
}