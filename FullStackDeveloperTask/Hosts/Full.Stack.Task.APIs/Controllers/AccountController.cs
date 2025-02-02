using Full.Stack.Task.Application.Features.Authentication.Commands.EmailAuthentication;
using Full.Stack.Task.Application.Features.Users.Queries.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Full.Stack.Task.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IMediator mediator) : ControllerBase
    {
        [HttpPost("[Action]")]
        public async Task<IActionResult> Login([FromBody] EmailAuthenticationCommand emailAuthenticationCommand)
        {
            return Ok(await mediator.Send(emailAuthenticationCommand));
        }

    }
}
