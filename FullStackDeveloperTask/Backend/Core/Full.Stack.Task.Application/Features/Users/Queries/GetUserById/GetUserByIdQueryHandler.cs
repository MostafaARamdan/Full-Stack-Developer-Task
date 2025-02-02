using Full.Stack.Task.Application.Contracts.Persistence.Repositories;
using Full.Stack.Task.Application.DTOs;
using Full.Stack.Task.Application.Messaging;
using Full.Stack.Task.Domain.GeneralResponse;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Full.Stack.Task.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler(
        IUserRepository _userRepository,
        IRoleRepository _roleRepository,
        IMapper _mapper) : IQueryHandler<GetUserByIdQuery, GetUserByIdResponse>
    {
        public async Task<Result<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var model = await _userRepository.GetUserByIdAsync(request.Id);
                if (model == null)
                    return Result<GetUserByIdResponse>.Error("User_not_found");
                var response = new GetUserByIdResponse()
                {
                    Roles = _mapper.Map<List<RoleDTO>>(await _roleRepository.GetAll())
                };
                response.User = _mapper.Map<UserDetailsDTO>(model); ;

                return Result<GetUserByIdResponse>.Success(response, "Success");
            }
            catch
            {
                throw;
            }
        }
    }
}
