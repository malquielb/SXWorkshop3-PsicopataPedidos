using PsicopataPedidos.OrdersManagement.Application.Services.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Application.Contracts.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryResponseDto>> ListAllCategories();
        Task<CategoryResponseDto> GetCategoryById(int id);
        Task<CategoryResponseDto> AddCategory(CategoryRequestDto categoryResquest);
        Task<CategoryResponseDto> UpdateCategory(int id, CategoryRequestDto categoryRequest);
        Task DeleteCategory(int id);
    }
}
