using LanguageBuilder.Data.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageBuilder.Services.Contracts
{
    public interface IUsersService
    {
        User GetByIdentity(string identityName);
        Task<User> GetByIdentityAsync(string identityName);

    }
}
