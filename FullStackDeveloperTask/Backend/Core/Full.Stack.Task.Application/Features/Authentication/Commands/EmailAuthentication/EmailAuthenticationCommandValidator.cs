using FluentValidation;
using Full.Stack.Task.Application.Contracts.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Full.Stack.Task.Application.Features.Authentication.Commands.EmailAuthentication
{
    internal class EmailAuthenticationCommandValidator : AbstractValidator<EmailAuthenticationCommand>
    {
        public EmailAuthenticationCommandValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password_is_required.");


            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Email_is_required.");

        }
        
    }
}
