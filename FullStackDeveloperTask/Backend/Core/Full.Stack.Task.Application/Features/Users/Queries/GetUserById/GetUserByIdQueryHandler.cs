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
                var response = new GetUserByIdResponse()
                {
                    Roles = _mapper.Map<List<RoleDTO>>(await _roleRepository.GetAll()),
                    User = new UserDetailsDTO() { Email = "", FullName = "", IsDeleted = false, Username = "" }
                };

                if (request.Id.HasValue)
                {
                    var model = await _userRepository.GetUserByIdAsync(request.Id.Value);
                    if (model != null)
                        response.User = _mapper.Map<UserDetailsDTO>(model);
                }
                return Result<GetUserByIdResponse>.Success(response, "Success");
            }
            catch
            {
                throw;
            }
        }
    }
}
