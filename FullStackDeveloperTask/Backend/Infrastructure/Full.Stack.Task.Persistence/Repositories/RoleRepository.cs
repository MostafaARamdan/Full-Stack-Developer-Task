using Full.Stack.Task.Application.Contracts.Persistence.Repositories;
using Full.Stack.Task.Domain.Entities;
using Full.Stack.Task.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Full.Stack.Task.Persistence.Repositories
{
    public class RoleRepository(ApplicationDbContext _context) : IRoleRepository
    {
        public async Task<List<Role>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }
        public async Task<bool> RolesExistsAsync(List<Guid> roleIds)
        {
            return await _context.Roles.Where(r => roleIds.Contains(r.Id)).CountAsync() == roleIds.Count;
        }
    }
}
