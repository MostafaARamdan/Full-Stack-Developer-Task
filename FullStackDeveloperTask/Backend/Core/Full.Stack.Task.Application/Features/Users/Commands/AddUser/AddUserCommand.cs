using Full.Stack.Task.Application.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Full.Stack.Task.Application.Features.Users.Commands.AddUser
{
    public class AddUserCommand : ICommand<bool>
    {
        public required string Username { get; set; } = string.Empty;
        public required string Password { get; set; } = string.Empty;
        public required string ConfirmPassword { get; set; } = string.Empty;
        public required string Email { get; set; } = string.Empty;
        public required string FullName { get; set; } = string.Empty;
        public required List<Guid> RoleIds { get; set; }
        public required Guid CreatedBy { get; set; } 
    }
}
