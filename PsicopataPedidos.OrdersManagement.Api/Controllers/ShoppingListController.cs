using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsicopataPedidos.OrdersManagement.Application.Contracts.Services;
using PsicopataPedidos.OrdersManagement.Application.Services.ShoppingList;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PsicopataPedidos.OrdersManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingListController : ControllerBase
    {
        private readonly IShoppingListService _shoppingListService;

        public ShoppingListController(IShoppingListService shoppingListService)
        {
            _shoppingListService = shoppingListService;
        }

        [HttpGet, Authorize(Roles = "Client")]
        public async Task<ActionResult<List<ShoppingListItemResponseDto>>> GetAll()
        {
            var result = await _shoppingListService.GetShoppingListItems();
            return result;
        }

        [HttpPost, Authorize(Roles = "Client")]
        public async Task<ActionResult<ShoppingListItemResponseDto>> Add([FromBody] ShoppingListItemRequestDto shoppingListItem)
        {
            var result = await _shoppingListService.AddShoppingListItem(shoppingListItem);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpDelete("{id}"), Authorize(Roles = "Client")]
        public async Task<ActionResult> Delete(int id)
        {
            await _shoppingListService.RemoveShoppingListItem(id);
            return NoContent();
        }
    }
}
