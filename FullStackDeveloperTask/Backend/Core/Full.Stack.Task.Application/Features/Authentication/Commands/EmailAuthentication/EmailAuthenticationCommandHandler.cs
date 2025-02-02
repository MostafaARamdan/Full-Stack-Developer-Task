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
using Full.Stack.Task.Application.Contracts.Services;
using Full.Stack.Task.Application.Services;

namespace Full.Stack.Task.Application.Features.Authentication.Commands.EmailAuthentication
{
    public class EmailAuthenticationCommandHandler(IUserRepository _userRepository, IMediator _mediator, IJwtService jwtService) : ICommandHandler<EmailAuthenticationCommand, AuthResponse>
    {
        public async Task<Result<AuthResponse>> Handle(EmailAuthenticationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByUsernameAsync(request.Username);
                if (user == null || user?.Password != await request.Password.Aragon2())
                {
                    return Result<AuthResponse>.Error("Invalid_Credential");
                }
                await _mediator.Publish(new AuditLogEvent(
                            tableName: nameof(User),
                            recordId: user.Id,
                            action: "Login",
                            oldValues: null,
                            newValues: JsonConvert.SerializeObject(request),
                            userId: user.Id
                      ), cancellationToken);
                return Result<AuthResponse>.Success(new AuthResponse {  Fullname = user.FullName, Token = jwtService.GenerateToken(user) }, "Success");
            }
            catch
            {
                throw;
            }
        }

    }
}
