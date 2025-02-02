using Full.Stack.Task.Application.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Full.Stack.Task.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : ICommand<bool>
    {
        public required Guid Id { get; set; }
        public Guid? DeletedBy { get; set; }
    }
}
