using System;
using System.Collections.Generic;
using System.Text;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Data;
using LanguageBuilder.Services.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LanguageBuilder.Services.Implementations
{
    public class UsersService : IUsersService
    {
        private readonly LanguageBuilderDbContext _db;

        public UsersService(LanguageBuilderDbContext context)
        {
            _db = context;
        }

        public User GetByIdentity(string identityName)
        {
            return _db
                .Users
                .Where(u => u.UserName == identityName)
                .FirstOrDefault();
        }

        public async Task<User> GetByIdentityAsync(string identityName)
        {
            return await _db
                .Users
                .Where(u => u.UserName == identityName)
                .FirstOrDefaultAsync();
        }
    }
}
