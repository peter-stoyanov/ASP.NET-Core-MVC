using cloudscribe.Pagination.Models;
using LanguageBuilder.Data;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Data.Services;
using LanguageBuilder.Services.Contracts;
using LanguageBuilder.Services.Models;
using LanguageBuilder.Services.Models.TranslationsSearch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LanguageBuilder.Services.Implementations
{
    public class TranslationService : BaseRepository<Translation, int>, ITranslationService
    {
        private readonly LanguageBuilderDbContext _db;

        public TranslationService(LanguageBuilderDbContext context)
            : base(context)
        {
            _db = context;
        }

        public async Task<IEnumerable<Translation>> GetByUserAndLanguageAsync(string userId, string languageId)
        {
            var targetWordIds = (await new WordsService(_db).GetByUserIdAsync(userId)).Select(w => w.Id);

            if (targetWordIds.Any())
            {
                return await _db
                    .Translations
                    .Include(t => t.SourceWord)
                    .Where(t => targetWordIds.Contains(t.TargetWordId))
                    .ToListAsync();
            }

            return Enumerable.Empty<Translation>();
        }

        public async Task<Response> Search(Request request, SortOptions sortOptions)
        {
            request.CancellationToken.ThrowIfCancellationRequested();

            int offset = (request.PageSize * request.PageNumber) - request.PageSize;
            if (offset < 0) { offset = 0; }

            var query = _db.Translations
                .Include(t => t.SourceWord)
                .Include(t => t.TargetWord)
                .AsQueryable();

            switch (sortOptions.SortColumn.ToLower())
            {
                case "source":
                    query = sortOptions.SortDirection == SortDirection.Ascending
                        ? query.OrderBy(t => t.SourceWord.Content)
                        : query.OrderByDescending(t => t.SourceWord.Content);
                    break;
                case "target":
                    query = sortOptions.SortDirection == SortDirection.Ascending
                        ? query.OrderBy(t => t.TargetWord.Content)
                        : query.OrderByDescending(t => t.TargetWord.Content);
                    break;
                default:
                    query = sortOptions.SortDirection == SortDirection.Ascending
                        ? query.OrderBy(t => t.SourceWord.Content)
                        : query.OrderByDescending(t => t.SourceWord.Content);
                    break;
            }

            if (request.Keywords != null)
            {
                query = query.Where(t => t.SourceWord.Content.Contains(request.Keywords.ToLower())
                    || t.TargetWord.Content.Contains(request.Keywords.ToLower()));
            }

            if (request.Filter != null)
            {
                query = query.Where(request.Filter);
            }

            if (request.SourceLanguageIds.Any())
            {
                query = query.Where(w => request.SourceLanguageIds.Contains(w.SourceWord.LanguageId));
            }

            if (request.TargetLanguageIds.Any())
            {
                query = query.Where(w => request.TargetLanguageIds.Contains(w.TargetWord.LanguageId));
            }

            var totalItems = await query.CountAsync();

            query = query
                .Select(p => p)
                .Skip(offset)
                .Take(request.PageSize);

            var pagedResult = new PagedResult<Translation>
            {
                Data = await query.AsNoTracking().ToListAsync(request.CancellationToken),
                TotalItems = totalItems,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            var response = new Response()
            {
                Records = pagedResult,
                TotalRecords = totalItems
            };

            return response;
        }

        public async Task<Translation> GetWithLoadedWordsAsync(int id)
        {
            return await _db
                .Translations
                .Include(t => t.SourceWord)
                .Include(t => t.TargetWord)
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
