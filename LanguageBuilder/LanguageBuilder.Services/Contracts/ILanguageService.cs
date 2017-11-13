using LanguageBuilder.Data.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageBuilder.Services.Contracts
{
    public interface ILanguageService
    {
        Task<IEnumerable<Language>> GetAllAsync();
    }
}
