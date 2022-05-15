using PsicopataPedidos.OrdersManagement.Application.Services.Categories;
using PsicopataPedidos.OrdersManagement.Application.Services.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Application.Contracts.Services
{
    public interface IProductService
    {
        Task<IReadOnlyCollection<ProductResponseDto>> ListAllProducts();
        Task<ProductResponseDto> GetProductById(int id);
        Task<ProductResponseDto> AddProduct(ProductRequestDto productRequest);
        Task<ProductResponseDto> UpdateProduct(int id, ProductRequestDto productRequest);
        Task DeleteProduct(int id);
    }
}
