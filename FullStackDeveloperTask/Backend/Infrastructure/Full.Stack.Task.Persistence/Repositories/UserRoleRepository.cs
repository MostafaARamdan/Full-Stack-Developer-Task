using Full.Stack.Task.Application.Contracts.Persistence.Repositories;
using Full.Stack.Task.Domain.Entities;
using Full.Stack.Task.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskNamespace = System.Threading.Tasks;

namespace Full.Stack.Task.Persistence.Repositories
{
    internal class UserRoleRepository(ApplicationDbContext _context) : IUserRoleRepository
    {
        public async TaskNamespace.Task AssignRoleToUserAsync(Guid userId, List<UserRole> userRoles, List<UserRole>? oldUserRoles = null)
        {
            if (oldUserRoles != null)
                _context.UserRoles.RemoveRange(oldUserRoles);

            await _context.UserRoles.AddRangeAsync(userRoles);

            await _context.SaveChangesAsync();

        }

    }
}
