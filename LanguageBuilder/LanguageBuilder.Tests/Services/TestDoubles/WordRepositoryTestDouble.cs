using LanguageBuilder.Data;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Data.Services;

namespace LanguageBuilder.Tests.Services.TestDoubles
{
    /// <summary>
    /// Concrete implementation used for testing the BaseRepository abstract class
    /// </summary>
    public class WordRepositoryTestDouble : BaseRepository<Word, int>
    {
        public WordRepositoryTestDouble(LanguageBuilderDbContext context)
            : base(context)
        {
        }
    }
}
