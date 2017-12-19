using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Models;
using System.Collections.Generic;

namespace LanguageBuilder.Services.Contracts
{
    public interface ILanguageService : IRepository<Language, int>, IAsyncRepository<Language, int>
    {
        IEnumerable<LanguageListingServiceModel> GetAllWithWordsCount();
    }
}
