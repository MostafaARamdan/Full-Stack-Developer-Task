using Full.Stack.Task.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskNamespace = System.Threading.Tasks;

namespace Full.Stack.Task.Application.Contracts.Persistence.Repositories
{
    public interface IUserRoleRepository
    {
        TaskNamespace.Task AssignRoleToUserAsync(Guid userId, List<UserRole> userRoles, List<UserRole>? oldUserRoles = null);
    }
}
