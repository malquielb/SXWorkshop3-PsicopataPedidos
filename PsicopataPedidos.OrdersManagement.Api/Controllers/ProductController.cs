using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsicopataPedidos.OrdersManagement.Application.Contracts.Services;
using PsicopataPedidos.OrdersManagement.Application.Services.Products;

namespace PsicopataPedidos.OrdersManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet, Authorize(Roles = "Admin,Client")]
        public async Task<ActionResult<List<ProductResponseDto>>> GetAll()
        {
            var result = await _productService.ListAllProducts();
            return Ok(result);
        }

        [HttpGet("{id}"), Authorize(Roles = "Admin,Client")]
        public async Task<ActionResult<ProductResponseDto>> GetById(int id)
        {
            var result = await _productService.GetProductById(id);
            return Ok(result);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductResponseDto>> Add(ProductRequestDto product)
        {
            var result = await _productService.AddProduct(product);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductResponseDto>> Update(int id, ProductRequestDto product)
        {
            var result = await _productService.UpdateProduct(id, product);
            return Ok(result);
        }

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            await _productService.DeleteProduct(id);
            return NoContent();
        }
    }
}
