using LanguageBuilder.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageBuilder.Services.Contracts
{
    public interface ITranslationService
    { 
        //Task<Translation> GetByWordsAsync(Word source, Word target);

        Task<IEnumerable<Translation>> GetByUserAndLanguageAsync(string userId, string languageId);
    }
}
