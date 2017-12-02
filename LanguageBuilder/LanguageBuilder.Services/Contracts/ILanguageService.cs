using LanguageBuilder.Data.Models;

namespace LanguageBuilder.Services.Contracts
{
    public interface ILanguageService : IRepository<Language, int>, IAsyncRepository<Language, int>
    {
    }
}
