using Full.Stack.Task.Application.Contracts.Persistence.Repositories;
using Full.Stack.Task.Application.Features.Users.Queries.GetUsers;
using Full.Stack.Task.Domain.Entities;
using Full.Stack.Task.Persistence.Contexts;
using Full.Stack.Task.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Full.Stack.Task.Persistence.Repositories
{
    public class UserRepository(ApplicationDbContext _context) : IUserRepository
    {

        public async Task<Guid> AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }

        public async Task<bool> UserExistsByUsernameAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }

        public async Task<bool> UserExistsByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> UserExistsAndIsActiveAsync(Guid userId)
        {
            return await _context.Users.AnyAsync(u => u.Id == userId && !u.IsDeleted);
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.Include(u => u.UserRoles).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> AnyUsersModifiedByAsync(Guid id)
        {
            return await _context.Users.AnyAsync(u => u.CreatedBy == id || u.ModifiedBy == id);
        }

        public async System.Threading.Tasks.Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserExistsByEmailAsync(string email, Guid userId)
        {
            return await _context.Users.AnyAsync(u => u.Email == email & u.Id != userId);
        }

        public async Task<bool> UserExistsByUsernameAsync(string username, Guid userId)
        {
            return await _context.Users.AnyAsync(u => u.Username == username & u.Id != userId);
        }

        public async System.Threading.Tasks.Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetUsers(GetUsersQuery request)
        {
            var query = _context.Users.Include(u => u.UserRoles).AsQueryable();

            if (!string.IsNullOrEmpty(request.Username))
            {
                query = query.Where(u => u.Username.Contains(request.Username));
            }

            if (!string.IsNullOrEmpty(request.FullName))
            {
                query = query.Where(u => u.FullName.Contains(request.FullName));
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                query = query.Where(u => u.Email.Contains(request.Email));
            }

            if (request.IsDeleted.HasValue)
            {
                query = query.Where(u => u.IsDeleted == request.IsDeleted.Value);
            }

            if (request.RoleId.HasValue)
            {
                query = query.Where(u => u.UserRoles.Any(ur => ur.RoleId == request.RoleId.Value));
            }

            if (!string.IsNullOrEmpty(request.MultipleSortWithDir))
            {
                var sortFields = request.MultipleSortWithDir.Split(',');
                foreach (var sortField in sortFields)
                {
                    var field = sortField.Trim();
                    if (field.StartsWith("-"))
                    {
                        query = query.OrderByDescending(field.Substring(1));
                    }
                    else
                    {
                        query = query.OrderBy(field);
                    }
                }
            }

            // Apply pagination
            query = query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);

            return await query.ToListAsync();
        }

        public async Task<int> GetUsersCount(GetUsersQuery request)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(request.Username))
            {
                query = query.Where(u => u.Username.Contains(request.Username));
            }

            if (!string.IsNullOrEmpty(request.FullName))
            {
                query = query.Where(u => u.FullName.Contains(request.FullName));
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                query = query.Where(u => u.Email.Contains(request.Email));
            }

            if (request.IsDeleted.HasValue)
            {
                query = query.Where(u => u.IsDeleted == request.IsDeleted.Value);
            }

            if (request.RoleId.HasValue)
            {
                query = query.Where(u => u.UserRoles.Any(ur => ur.RoleId == request.RoleId.Value));
            }

            return await query.CountAsync();
        }
        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
