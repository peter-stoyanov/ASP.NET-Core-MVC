using LanguageBuilder.Data;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Data.Services;
using LanguageBuilder.Services.Contracts;

namespace LanguageBuilder.Services.Implementations
{
    public class LanguageService : BaseRepository<Language, int>, ILanguageService
    {
        private readonly LanguageBuilderDbContext _db;

        public LanguageService(LanguageBuilderDbContext context)
            : base(context)
        {
            _db = context;
        }
    }
}
