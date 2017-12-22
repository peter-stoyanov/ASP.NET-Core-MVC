using AutoMapper.QueryableExtensions;
using LanguageBuilder.Data;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Data.Services;
using LanguageBuilder.Services.Contracts;
using LanguageBuilder.Services.Models;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<LanguageListingServiceModel> GetAllWithWordsCount()
        {
            return _db.Languages.ProjectTo<LanguageListingServiceModel>().ToList();
        }
    }
}
