using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Full.Stack.Task.Application.DTOs
{
    public class UserDetailsDTO
    {
        public Guid Id { get; set; }
        public required string Username { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public DateTime Created { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? Modified { get; set; }
        public Guid? ModifiedBy { get; set; }
        public required bool IsDeleted { get; set; }
        public List<UserRoleDTO> UserRoles { get; set; }
    }
}
