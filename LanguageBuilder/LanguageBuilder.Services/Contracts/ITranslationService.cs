using LanguageBuilder.Data.Models;
using System.Threading.Tasks;

namespace LanguageBuilder.Services.Contracts
{
    public interface ITranslationService : IRepository<Translation, int>, IAsyncRepository<Translation, int>
    {
        Task<Translation> GetByWordsAsync(Word source, Word target);
    }
}
