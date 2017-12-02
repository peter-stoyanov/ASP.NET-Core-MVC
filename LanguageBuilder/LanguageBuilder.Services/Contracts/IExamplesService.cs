using LanguageBuilder.Data.Models;
using System.Threading.Tasks;

namespace LanguageBuilder.Services.Contracts
{
    public interface IExamplesService : IRepository<Example, int>, IAsyncRepository<Example, int>
    {
        Task AddInUserWord(Word word, User user, Example example);
    }
}
