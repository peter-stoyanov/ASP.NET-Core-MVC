using LanguageBuilder.Data.Models;
using System.Threading.Tasks;

namespace LanguageBuilder.Services.Contracts
{
    public interface IUsersService
    {
        User GetByIdentity(string identityName);

        Task<User> GetByIdentityAsync(string identityName);
    }
}
