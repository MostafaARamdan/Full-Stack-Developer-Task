using Full.Stack.Task.Application.DTOs;
using Full.Stack.Task.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Full.Stack.Task.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdResponse
    {
        public UserDetailsDTO User { get; set; } = null;
        public List<RoleDTO> Roles { get; set; } = new();
    }
}
