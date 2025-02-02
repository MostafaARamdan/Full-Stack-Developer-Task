using Full.Stack.Task.Application.Features.Users.Commands.AddUser;
using Full.Stack.Task.Application.Features.Users.Commands.DeleteUser;
using Full.Stack.Task.Application.Features.Users.Commands.EditUser;
using Full.Stack.Task.Application.Features.Users.Queries.GetUserById;
using Full.Stack.Task.Application.Features.Users.Queries.GetUsers;
using Full.Stack.Task.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Full.Stack.Task.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [HttpPost("[Action]")]
        public async Task<IActionResult> GetUsers([FromBody] GetUsersQuery usersQuery)
        {
            return Ok(await mediator.Send(usersQuery));
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(Guid? id)
        {
            return Ok(await mediator.Send(new GetUserByIdQuery() { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddUserCommand user)
        {
            return Ok(await mediator.Send(user));
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] EditUserCommand user)
        {
            return Ok(await mediator.Send(user));
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            return Ok(await mediator.Send(new DeleteUserCommand() { Id = Id }));
        }
    }
}
