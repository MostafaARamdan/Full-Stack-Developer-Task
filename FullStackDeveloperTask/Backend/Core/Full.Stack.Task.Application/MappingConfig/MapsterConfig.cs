using Full.Stack.Task.Application.DTOs;
using Full.Stack.Task.Application.Features.Users.Commands.AddUser;
using Full.Stack.Task.Application.Features.Users.Queries.GetUserById;
using Full.Stack.Task.Application.Features.Users.Queries.GetUsers;
using Full.Stack.Task.Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Full.Stack.Task.Application.MappingConfig
{
    public static class MapsterConfig
    {
        public static void Configure()
        {
            TypeAdapterConfig<User, UserDetailsDTO>
                .NewConfig()
                .Map(dest => dest.UserRoles, src => src.UserRoles.Select(userRole => new UserRoleDTO { RoleId = userRole.RoleId }).ToList());

            TypeAdapterConfig<User, UserDTO>
                .NewConfig()
                .Map(dest => dest.UserRoles, src => src.UserRoles.Select(userRole => new UserRoleDTO { RoleId = userRole.RoleId, RoleName = userRole.Role.Name }).ToList());

            TypeAdapterConfig<AddUserCommand, User>
             .NewConfig()
             .Map(dest => dest.UserRoles, src => src.RoleIds.DistinctBy(roleId => roleId).Select(roleId => new UserRole { RoleId = roleId }));
        }
    }
}
