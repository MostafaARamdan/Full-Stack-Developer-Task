using FluentValidation;
using Full.Stack.Task.Application.Contracts.Persistence.Repositories;
using Full.Stack.Task.Application.Features.Users.Commands.AddUser;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Full.Stack.Task.Application.Features.Users.Commands.EditUser
{
    internal class EditUserCommandValidator : AbstractValidator<EditUserCommand>
    {
        private readonly IUserRepository _userRepository; 
        private readonly IRoleRepository _roleRepository;
        private ValidationContext<EditUserCommand> _validationContext;
        protected override bool PreValidate(ValidationContext<EditUserCommand> context, FluentValidation.Results.ValidationResult result)
        {
            _validationContext = context;
            return base.PreValidate(context, result);
        }
        public EditUserCommandValidator(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username_is_required.")
                .Length(3, 100).WithMessage("Username_must_be_between_3_and_100_characters.")
                .MustAsync(BeUniqueUsername).WithMessage("Username_already_exists.");

            RuleFor(x => x.Password)
                .NotEmpty().When(x => !string.IsNullOrEmpty(x.ConfirmPassword)).WithMessage("Password_is_required.")
                .MinimumLength(6).When(x => !string.IsNullOrEmpty(x.Password)).WithMessage("Password_must_be_at_least_6_characters.")
                .Matches(@"[A-Z]").When(x => !string.IsNullOrEmpty(x.Password)).WithMessage("Password_must_contain_at_least_one_uppercase_letter.")
                .Matches(@"[a-z]").When(x => !string.IsNullOrEmpty(x.Password)).WithMessage("Password_must_contain_at_least_one_lowercase_letter.")
                .Matches(@"\d").When(x => !string.IsNullOrEmpty(x.Password)).WithMessage("Password_must_contain_at_least_one_number.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().When(x => !string.IsNullOrEmpty(x.Password)).WithMessage("Confirm_Password_is_required.")
                .Equal(x => x.Password).When(x => !string.IsNullOrEmpty(x.Password)).WithMessage("Confirm_Password_must_match_Password.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email_is_required.")
                .EmailAddress().WithMessage("Invalid_email_format.")
                .MustAsync(BeUniqueEmail).WithMessage("Email_already_exists.");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full_name_is_required.")
                .Length(3, 255).WithMessage("Full_name_must_be_between_3_and_255_characters.");


            RuleFor(x => x.RoleIds) 
                .NotEmpty().WithMessage("RoleId_is_required.")
                .MustAsync(RoleExists).WithMessage("Invalid_RoleId._Role_does_not_exist.");

        }
        private async Task<bool> BeUniqueUsername(string username,  CancellationToken cancellationToken)
        {
            return !await _userRepository.UserExistsByUsernameAsync(username, _validationContext.InstanceToValidate.Id);
        }

        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            return !await _userRepository.UserExistsByEmailAsync(email, _validationContext.InstanceToValidate.Id);
        }
        private async Task<bool> ExistsAndIsActiveUser(Guid createdBy, CancellationToken cancellationToken)
        {
            return await _userRepository.UserExistsAndIsActiveAsync(createdBy);
        }
        private async Task<bool> RoleExists(List<Guid> roleIds, CancellationToken cancellationToken)
        {
            return await _roleRepository.RolesExistsAsync(roleIds);
        }
        
    }
}
