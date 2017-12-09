using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Models.UsersSearch;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LanguageBuilder.Services.Contracts
{
    public interface IRolesService
    {
        IEnumerable<Role> GetAll();
        Task<IEnumerable<Role>> GetAllAsync();

        IEnumerable<Role> GetByUserId(string id);
        Task<IEnumerable<Role>> GetByUserIdAsync(string id);

        Task<Role> GetByIdAsync(string id);
        Task<Role> GetIdByNameAsync(string role);

        Task AddAsync(string userId, IEnumerable<string> roles);
        Task AddAsync(string userId, string role);

        Task DeleteByUserIdAsync(string userId);

        Task UpdateAsync(string userId, IEnumerable<string> roles);

        Task<bool> IsInRoleAsync(string userId, string role);
    }
}
