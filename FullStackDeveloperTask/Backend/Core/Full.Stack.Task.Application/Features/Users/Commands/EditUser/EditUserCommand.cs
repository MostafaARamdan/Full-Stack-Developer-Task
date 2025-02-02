using Full.Stack.Task.Application.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Full.Stack.Task.Application.Features.Users.Commands.EditUser
{
    public class EditUserCommand : ICommand<bool>
    {
        public Guid Id { get; set; }
        public required string Username { get; set; } = string.Empty;
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; } 
        public required string Email { get; set; } = string.Empty;
        public required string FullName { get; set; } = string.Empty;
        public required List<Guid> RoleIds { get; set; }
    }
}
