using LanguageBuilder.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageBuilder.Services.Contracts
{
    public interface ITranslationService
    {
        Task<IEnumerable<Translation>> GetByUserAndLanguageAsync(string userId, string languageId);
    }
}
