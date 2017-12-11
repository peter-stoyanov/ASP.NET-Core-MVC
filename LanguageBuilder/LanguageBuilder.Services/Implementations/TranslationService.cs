using cloudscribe.Pagination.Models;
using LanguageBuilder.Data;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Data.Services;
using LanguageBuilder.Services.Contracts;
using LanguageBuilder.Services.Models.WordsSearch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace LanguageBuilder.Services.Implementations
{
    public class TranslationService : ITranslationService
    {
        private readonly LanguageBuilderDbContext _db;

        public TranslationService(LanguageBuilderDbContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<Translation>> GetByUserAndLanguageAsync(string userId, string languageId)
        {
            var targetWordIds = (await new WordsService(_db).GetByUserIdAsync(userId)).Select(w => w.Id);

            if (targetWordIds.Any())
            {
                return await _db.Translations.Where(t => targetWordIds.Contains(t.TargetWordId)).ToListAsync();
            }

            return Enumerable.Empty<Translation>();
        }

        //public Task<Translation> GetByWordsAsync(Word source, Word target)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
