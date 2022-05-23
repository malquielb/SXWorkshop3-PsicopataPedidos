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
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(PsicopataPedidosDbContext context) : base(context)
        {
        }

        public override async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Set<Product>()
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public override async Task<IReadOnlyCollection<Product>> ListAllAsync()
        {
            return await _context.Set<Product>()
                .Include(p => p.Categories)
                .ToListAsync();
        }
    }
}
