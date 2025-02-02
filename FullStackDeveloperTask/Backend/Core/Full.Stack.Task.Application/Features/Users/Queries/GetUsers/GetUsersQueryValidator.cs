using FluentValidation;
using Full.Stack.Task.Application.Contracts.Persistence.Repositories;
using Full.Stack.Task.Application.Features.Users.Commands.DeleteUser;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Full.Stack.Task.Application.Features.Users.Queries.GetUsers
{
    internal class GetUsersQueryValidator : AbstractValidator<GetUsersQuery>
    {
        public GetUsersQueryValidator()
        {
            RuleFor(x => x.PageNumber).NotNull().GreaterThanOrEqualTo(1).WithMessage("PageNumber_must_be_greater_than_1");
            RuleFor(x => x.PageSize).NotNull().GreaterThanOrEqualTo(1).WithMessage("PageSize_must_be_greater_than_1");
        }

    }
}
