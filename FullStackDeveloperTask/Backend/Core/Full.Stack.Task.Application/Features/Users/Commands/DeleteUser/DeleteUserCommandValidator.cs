using FluentValidation;
using Full.Stack.Task.Application.Contracts.Persistence.Repositories;
using Full.Stack.Task.Application.Features.Users.Commands.DeleteUser;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Full.Stack.Task.Application.Features.Users.Commands.DeleteUser
{
    internal class GetUsersQueryValidator : AbstractValidator<DeleteUserCommand>
    {
        public GetUsersQueryValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("UserId_is_required");
        }

    }
}
