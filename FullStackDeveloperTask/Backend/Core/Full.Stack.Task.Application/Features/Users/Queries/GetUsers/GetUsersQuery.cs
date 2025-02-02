using Full.Stack.Task.Application.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Full.Stack.Task.Application.Features.Users.Queries.GetUsers
{
    public class GetUsersQuery : IQuery<GetUsersResponse>
    {
        public string? Username { get; set; } = string.Empty;
        public string? FullName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public bool? IsDeleted { get; set; } = null;
        public Guid? RoleId { get; set; }
        public string? MultipleSortWithDir { get; set; } = string.Empty;
        public required int PageNumber { get; set; }
        public required int PageSize { get; set; }
    }
}
