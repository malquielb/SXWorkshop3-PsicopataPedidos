using Microsoft.EntityFrameworkCore;
using PsicopataPedidos.OrdersManagement.Application.Contracts.Persistence;
using PsicopataPedidos.OrdersManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Persistence.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(PsicopataPedidosDbContext context) : base(context)
        {
        }

        public async Task<IReadOnlyCollection<Order>> GetOrdersForUser(int userId)
        {
            return await _context.Orders.Where(o => o.UserId == userId)
                    .ToListAsync();
        }
    }
}
