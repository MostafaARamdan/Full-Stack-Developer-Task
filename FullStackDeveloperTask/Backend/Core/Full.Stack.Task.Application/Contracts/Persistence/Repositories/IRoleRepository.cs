using Full.Stack.Task.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Full.Stack.Task.Application.Contracts.Persistence.Repositories
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetAll();
        Task<bool> RolesExistsAsync(List<Guid> roleId);
    }
}
