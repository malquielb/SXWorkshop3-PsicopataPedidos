using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsicopataPedidos.OrdersManagement.Application.Contracts.Services;
using PsicopataPedidos.OrdersManagement.Application.Services.Categories;

namespace PsicopataPedidos.OrdersManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet, Authorize(Roles = "Admin,Client")]
        public async Task<ActionResult<List<CategoryResponseDto>>> GetAll()
        {
            var result = await _categoryService.ListAllCategories();
            return Ok(result);
        }

        [HttpGet("{id}"), Authorize(Roles = "Admin,Client")]
        public async Task<ActionResult<CategoryResponseDto>> GetById(int id)
        {
            var result = await _categoryService.GetCategoryById(id);
            return Ok(result);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<CategoryResponseDto>> Add(CategoryRequestDto category)
        {
            var result = await _categoryService.AddCategory(category);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<CategoryResponseDto>> Update(int id, CategoryRequestDto category)
        {
            var result = await _categoryService.UpdateCategory(id, category);
            return Ok(result);
        }

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            await _categoryService.DeleteCategory(id);
            return NoContent();
        }
    }
}
