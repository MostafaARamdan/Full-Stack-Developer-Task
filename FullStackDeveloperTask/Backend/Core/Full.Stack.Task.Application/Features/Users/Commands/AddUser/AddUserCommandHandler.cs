using Full.Stack.Task.Application.Contracts.Persistence.Repositories;
using Full.Stack.Task.Application.Extensions;
using Full.Stack.Task.Application.Messaging;
using Full.Stack.Task.Domain.Entities;
using Full.Stack.Task.Domain.GeneralResponse;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tasks = System.Threading.Tasks;
using Newtonsoft.Json;
using Full.Stack.Task.Application.Events.AuditLogs;
using Full.Stack.Task.Application.Features.Users.Queries.GetUserById;
using Full.Stack.Task.Application.DTOs;

namespace Full.Stack.Task.Application.Features.Users.Commands.AddUser
{
    public class AddUserCommandHandler(IMapper _mapper, IUserRepository _userRepository, IMediator _mediator) : ICommandHandler<AddUserCommand, bool>
    {
        public async Task<Result<bool>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                User user = _mapper.Map<User>(request);
                user.Password = await user.Password.Aragon2() ?? string.Empty;
                await _userRepository.AddAsync(user);
                var dd = _mapper.Map<UserDetailsDTO>(user);
                await _mediator.Publish(new AuditLogEvent(
                            tableName: nameof(User),
                            recordId: user.Id,
                            action: "Insert",
                            oldValues: null,
                            newValues: JsonConvert.SerializeObject(dd),
                            userId: request.CreatedBy 
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
