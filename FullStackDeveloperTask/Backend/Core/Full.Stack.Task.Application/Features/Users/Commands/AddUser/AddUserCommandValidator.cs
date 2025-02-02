using FluentValidation;
using Full.Stack.Task.Application.Contracts.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Full.Stack.Task.Application.Features.Users.Commands.AddUser
{
    internal class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        private readonly IUserRepository _userRepository; 
        private readonly IRoleRepository _roleRepository;
        public AddUserCommandValidator(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username_is_required.")
                .Length(3, 100).WithMessage("Username_must_be_between_3_and_100_characters.")
                .MustAsync(BeUniqueUsername).WithMessage("Username_already_exists.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password_is_required.")
                .MinimumLength(6).WithMessage("Password_must_be_at_least_6_characters.")
                .Matches(@"[A-Z]").WithMessage("Password_must_contain_at_least_one_uppercase_letter.")
                .Matches(@"[a-z]").WithMessage("Password_must_contain_at_least_one_lowercase_letter.")
                .Matches(@"\d").WithMessage("Password_must_contain_at_least_one_number.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm_Password_is_required.")
                .Equal(x => x.Password).WithMessage("Confirm_Password_must_match_Password.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email_is_required.")
                .EmailAddress().WithMessage("Invalid_email_format.")
                .MustAsync(BeUniqueEmail).WithMessage("Email_already_exists.");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full_name_is_required.")
                .Length(3, 255).WithMessage("Full_name_must_be_between_3_and_255_characters.");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("CreatedBy_is_required.")
                .MustAsync(ExistsAndIsActiveUser).WithMessage("CreatedBy_must_be_a_valid_and_active_user.");


            RuleFor(x => x.RoleIds) 
                .NotEmpty().WithMessage("RoleId_is_required.")
                .MustAsync(RoleExists).WithMessage("Invalid_RoleId._Role_does_not_exist.");

        }
        private async Task<bool> BeUniqueUsername(string username, CancellationToken cancellationToken)
        {
            return !await _userRepository.UserExistsByUsernameAsync(username);
        }

        // 🔹 Custom Validation to Check Unique Email
        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            return !await _userRepository.UserExistsByEmailAsync(email);
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
