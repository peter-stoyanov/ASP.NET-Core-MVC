using LanguageBuilder.Data;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageBuilder.Services.Implementations
{
    public class RolesService : IRolesService
    {
        private readonly LanguageBuilderDbContext _db;

        public RolesService(LanguageBuilderDbContext context)
        {
            _db = context;
        }

        public IEnumerable<Role> GetAll()
        {
            return _db.Roles.ToList();
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _db.Roles.ToListAsync();
        }

        public IEnumerable<Role> GetByUserId(string id)
        {
            return _db
                .UserRoles
                .Where(ur => ur.UserId == id)
                .Select(ur => new Role
                {
                    Id = ur.RoleId,
                    Name = _db.Roles.Where(r => r.Id == ur.RoleId).FirstOrDefault().Name
                })
                .ToList();
        }

        public async Task<IEnumerable<Role>> GetByUserIdAsync(string id)
        {
            return await _db
                .UserRoles
                .Where(ur => ur.UserId == id)
                .Select(ur => new Role
                {
                    Id = ur.RoleId,
                    Name = _db.Roles.Where(r => r.Id == ur.RoleId).FirstOrDefault().Name
                })
                .ToListAsync();
        }

        public async Task AddAsync(string userId, IEnumerable<string> roles)
        {
            foreach (var role in roles)
            {
                await AddAsync(userId, role);
            }
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            return (await GetByUserIdAsync(userId)).Any(r => r.Name.ToLower() == role.ToLower());
        }

        public async Task<Role> GetByIdAsync(string id)
        {
            return await _db
                .Roles
                .Where(r => r.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Role> GetIdByNameAsync(string role)
        {
            return await _db
                .Roles
                .Where(r => r.Name == role)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(string userId, string role)
        {
            if (await IsInRoleAsync(userId, role)) { return; }

            _db.UserRoles.Add(new IdentityUserRole<string>
            {
                RoleId = (await GetIdByNameAsync(role)).Id,
                UserId = userId
            });

            await _db.SaveChangesAsync();
        }

        public async Task DeleteByUserIdAsync(string userId)
        {
            var userRoles = _db.UserRoles.Where(ur => ur.UserId == userId).ToList();

            foreach (var role in userRoles)
            {
                _db.UserRoles.Remove(role);
            }

            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(string userId, IEnumerable<string> roles)
        {
            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var userRoles = _db.UserRoles.Where(ur => ur.UserId == userId).ToList();

                    foreach (var role in userRoles)
                    {
                        _db.UserRoles.Remove(role);
                    }

                    await _db.SaveChangesAsync();

                    foreach (var role in roles)
                    {
                        _db.UserRoles.Add(new IdentityUserRole<string>
                        {
                            RoleId = (await GetIdByNameAsync(role)).Id,
                            UserId = userId
                        });
                    }

                    await _db.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
