using Full.Stack.Task.Application.Features.Users.Commands.AddUser;
using Full.Stack.Task.Application.Features.Users.Commands.DeleteUser;
using Full.Stack.Task.Application.Features.Users.Commands.EditUser;
using Full.Stack.Task.Application.Features.Users.Queries.GetUserById;
using Full.Stack.Task.Application.Features.Users.Queries.GetUsers;
using Full.Stack.Task.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Full.Stack.Task.APIs.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [HttpPost("[Action]")]
        public async Task<IActionResult> GetUsers([FromBody] GetUsersQuery usersQuery)
        {
            return Ok(await mediator.Send(usersQuery));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Get")]
        public async Task<IActionResult> Get(Guid? id)
        {
            return Ok(await mediator.Send(new GetUserByIdQuery() { Id = id }));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddUserCommand user)
        {
            return Ok(await mediator.Send(user));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] EditUserCommand user)
        {
            return Ok(await mediator.Send(user));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            return Ok(await mediator.Send(new DeleteUserCommand() { Id = Id }));
        }
    }
}
