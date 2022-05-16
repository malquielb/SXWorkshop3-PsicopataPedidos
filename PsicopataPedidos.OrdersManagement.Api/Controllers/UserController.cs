using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsicopataPedidos.OrdersManagement.Application.Contracts.Services;

namespace PsicopataPedidos.OrdersManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
           _userService = userService;
        }

        [HttpPost("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> FundWallet(int id, [FromBody] decimal amount)
        {
            await _userService.FundClientWallet(id, amount);
            return NoContent();
        }
    }
}
