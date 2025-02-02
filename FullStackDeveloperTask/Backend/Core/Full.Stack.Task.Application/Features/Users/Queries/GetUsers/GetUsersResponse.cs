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
        public List<UserDetailsDTO> Users { get; set; } = new();
        public List<RoleDTO> Roles { get; set; } = new();
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
    }

}
