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
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(PsicopataPedidosDbContext context) : base(context)
        {
        }

        public async Task<Category> GetCategoryByName(string categoryName)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Name == categoryName);
        }
    }
}
