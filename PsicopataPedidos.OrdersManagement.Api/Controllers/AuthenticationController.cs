using Microsoft.AspNetCore.Mvc;
using PsicopataPedidos.OrdersManagement.Application.Contracts.Identity;
using PsicopataPedidos.OrdersManagement.Identity.Services;

namespace PsicopataPedidos.OrdersManagement.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register(UserRegisterDto userRegister)
        {
            await _authenticationService.Register(userRegister);
            return NoContent();
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string>> Login(UserLoginDto userLogin)
        {
            var jwt = await _authenticationService.Login(userLogin);
            return Ok(jwt);
        }
    }
}
