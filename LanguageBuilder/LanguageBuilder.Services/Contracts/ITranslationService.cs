using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Models;
using LanguageBuilder.Services.Models.TranslationsSearch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageBuilder.Services.Contracts
{
    public interface ITranslationService : IRepository<Translation, int>, IAsyncRepository<Translation, int>
    {
        Task<IEnumerable<Translation>> GetByUserAndLanguageAsync(string userId, string languageId);

        Task<Response> Search(Request request, SortOptions sortOptions);

        Task<Translation> GetWithLoadedWordsAsync(int id);
    }
}
