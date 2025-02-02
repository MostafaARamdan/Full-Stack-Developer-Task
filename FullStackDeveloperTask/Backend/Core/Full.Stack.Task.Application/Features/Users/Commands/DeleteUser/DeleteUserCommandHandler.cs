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

namespace Full.Stack.Task.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler(IUserRepository _userRepository, IMediator _mediator, IMapper _mapper) : ICommandHandler<DeleteUserCommand, bool>
    {
        public async Task<Result<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(request.Id);
                if (user == null)
                    return Result<bool>.Error("User_not_found");
                if (user.IsDeleted)
                    return Result<bool>.Error("User_already_deleted");
                var oldValues = JsonConvert.SerializeObject(_mapper.Map<UserDetailsDTO>(user));
                if (await _userRepository.AnyUsersModifiedByAsync(request.Id))
                {
                    user.IsDeleted = true;
                    await _userRepository.UpdateAsync(user);
                    await _mediator.Publish(new AuditLogEvent(
                            tableName: nameof(User),
                            recordId: user.Id,
                            action: "Deactivate",
                            oldValues: oldValues,
                            newValues: null,
                            userId: request.DeletedBy
                        ), cancellationToken);
                    return Result<bool>.Error("User_has_transactions_deactivated");

                }
                await _userRepository.DeleteAsync(user);
                await _mediator.Publish(new AuditLogEvent(
                           tableName: nameof(User),
                           recordId: user.Id,
                           action: "Delete",
                           oldValues: oldValues,
                           newValues: null,
                           userId: request.DeletedBy
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
