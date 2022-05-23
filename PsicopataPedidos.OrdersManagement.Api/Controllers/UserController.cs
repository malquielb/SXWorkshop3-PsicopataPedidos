using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsicopataPedidos.OrdersManagement.Application.Contracts.Services;
using PsicopataPedidos.OrdersManagement.Application.Services.Users;

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

        [HttpPost("FundWallet/{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> FundWallet(int id, [FromBody] decimal amount)
        {
            await _userService.FundClientWallet(id, amount);
            return NoContent();
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
            var result = await _userService.GetAllUsers();
            return Ok(result);
        }

        [HttpGet("Profile"), Authorize(Roles = "Admin,Client")]
        public async Task<ActionResult<List<UserDto>>> GetProfile()
        {
            var result = await _userService.GetUserProfile();
            return Ok(result);
        }
    }
}
