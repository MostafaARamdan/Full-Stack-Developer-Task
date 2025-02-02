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

namespace Full.Stack.Task.Application.Features.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler(
        IUserRepository _userRepository,
        IRoleRepository _roleRepository,
        IMapper _mapper) : IQueryHandler<GetUsersQuery, GetUsersResponse>
    {
        public async Task<Result<GetUsersResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new GetUsersResponse()
                {
                    Roles = _mapper.Map<List<RoleDTO>>(await _roleRepository.GetAll()),
                    Users = _mapper.Map<List<UserDTO>>(await _userRepository.GetUsers(request)),
                    PagesCount = await _userRepository.GetUsersCount(request),
                    CurrentPage = request.PageNumber
                };
                return Result<GetUsersResponse>.Success(response, "Success");
            }
            catch
            {
                throw;
            }
        }
    }
}
