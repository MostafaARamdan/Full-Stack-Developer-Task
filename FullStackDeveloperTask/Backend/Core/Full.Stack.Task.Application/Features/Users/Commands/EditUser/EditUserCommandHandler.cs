using Full.Stack.Task.Application.Contracts.Persistence.Repositories;
using Full.Stack.Task.Application.DTOs;
using Full.Stack.Task.Application.Events.AuditLogs;
using Full.Stack.Task.Application.Extensions;
using Full.Stack.Task.Application.Messaging;
using Full.Stack.Task.Domain.Entities;
using Full.Stack.Task.Domain.GeneralResponse;
using MapsterMapper;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tasks = System.Threading.Tasks;

namespace Full.Stack.Task.Application.Features.Users.Commands.EditUser
{
    public class EditUserCommandHandler(IUserRepository _userRepository, IUserRoleRepository _userRoleRepository, IMediator _mediator, IMapper _mapper) : ICommandHandler<EditUserCommand, bool>
    {
        public async Task<Result<bool>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(request.Id);
                if (user == null)
                    return Result<bool>.Error("User_not_found");
                if (user.IsDeleted)
                    return Result<bool>.Error("User_already_deleted");
                var oldValues = JsonConvert.SerializeObject(_mapper.Map<UserDetailsDTO>(user));
                if (!string.IsNullOrEmpty(request.Username)) user.Username = request.Username;
                if (!string.IsNullOrEmpty(request.Email)) user.Email = request.Email;
                if (!string.IsNullOrEmpty(request.FullName)) user.FullName = request.FullName;
                if (!string.IsNullOrEmpty(request.Password)) user.Password = await request.Password.Aragon2() ?? string.Empty;

                user.ModifiedBy = Guid.Parse("85ED7233-602F-47E3-AFCB-B1AA8BE36CF7");
                user.Modified = DateTime.UtcNow;

                await _userRepository.UpdateAsync(user);

                await _userRoleRepository.AssignRoleToUserAsync(user.Id, request.RoleIds.DistinctBy(roleId => roleId).Select(roleId =>
                new UserRole()
                {
                    RoleId = roleId,
                    UserId = user.Id
                }).ToList(), user.UserRoles.ToList());

                await _mediator.Publish(new AuditLogEvent(
                           tableName: nameof(User),
                           recordId: user.Id,
                           action: "Update",
                           oldValues: oldValues,
                           newValues: JsonConvert.SerializeObject(_mapper.Map<UserDetailsDTO>(user)),
                           userId: user.ModifiedBy
                       ), cancellationToken);
                return Result<bool>.Success(true, "Success");
            }
            catch
            {
                throw;
            }
        }

    }
}
