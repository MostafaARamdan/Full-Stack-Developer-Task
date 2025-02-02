using Full.Stack.Task.Application.DTOs;
using Full.Stack.Task.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Full.Stack.Task.Application.Features.Users.Queries.GetUsers
{
    public class GetUsersResponse
    {
        public List<UserDTO> Users { get; set; } = new();
        public List<RoleDTO> Roles { get; set; } = new();
        public int PagesCount { get; set; }
        public int CurrentPage { get; set; }
    }
    public class UserDTO
    {
        public Guid Id { get; set; }
        public required string Username { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required bool IsDeleted { get; set; }
        public List<UserRoleDTO> UserRoles { get; set; }
    }

}
