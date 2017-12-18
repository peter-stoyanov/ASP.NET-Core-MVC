using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Models.UsersSearch;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LanguageBuilder.Services.Contracts
{
    public interface IUsersService
    {
        User GetByIdentity(string identityName);

        Task<User> GetByIdentityAsync(string identityName);

        User GetById(string id);

        Task<User> GetByIdAsync(string id);

        Task<User> GetByUsernameAsync(string username);

        Task UpdateAsync(User user);

        Task<User> DeleteAsync(string id);

        Task<Response> Search(
            Request request,
            Expression<Func<User, object>> sortColumnSelector = null);
    }
}
