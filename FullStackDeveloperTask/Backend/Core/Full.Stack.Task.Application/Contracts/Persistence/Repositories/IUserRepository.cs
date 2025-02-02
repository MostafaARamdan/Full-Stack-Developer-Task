using Full.Stack.Task.Application.Features.Users.Queries.GetUsers;
using Full.Stack.Task.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskNamespace = System.Threading.Tasks;

namespace Full.Stack.Task.Application.Contracts.Persistence.Repositories
{
    public interface IUserRepository
    {
        Task<Guid> AddAsync(User user);
        TaskNamespace.Task UpdateAsync(User user);
        Task<User?> GetUserByIdAsync(Guid id);
        Task<bool> AnyUsersModifiedByAsync(Guid id);
        Task<bool> UserExistsAndIsActiveAsync(Guid value);
        Task<bool> UserExistsByEmailAsync(string email);
        Task<bool> UserExistsByEmailAsync(string email, Guid userId);
        Task<bool> UserExistsByUsernameAsync(string username);
        Task<bool> UserExistsByUsernameAsync(string username, Guid userId);
        TaskNamespace.Task DeleteAsync(User user);
        Task<List<User>> GetUsers(GetUsersQuery request);
        Task<int> GetUsersCount(GetUsersQuery request);
        Task<User?> GetByUsernameAsync(string username);
    }
}
