using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PsicopataPedidos.OrdersManagement.Application.Exceptions;

namespace PsicopataPedidos.OrdersManagement.Api.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var error = context.Error;
            var response = new ExceptionResponse();

            if (error.GetType() == typeof(ValidationException))
            {
                response.Details = ((ValidationException)error).ValidationErrors;
                response.StatusCode = StatusCodes.Status400BadRequest;
                return BadRequest(response);
            }

            if (error.GetType() == typeof(ApplicationException))
            {
                response.Details = new List<string>
                {
                    ((ApplicationException)error).Message
                };
                response.StatusCode = StatusCodes.Status400BadRequest;
                return BadRequest(response);
            }

            response.Details = new List<string> { error.Message };

            response.StatusCode = StatusCodes.Status500InternalServerError;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }

        private struct ExceptionResponse
        {
            public List<string> Details { get; set; }
            public int StatusCode { get; set; }
        }
    }
}
