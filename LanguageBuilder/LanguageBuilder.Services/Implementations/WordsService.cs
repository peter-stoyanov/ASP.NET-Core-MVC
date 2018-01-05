using cloudscribe.Pagination.Models;
using LanguageBuilder.Data;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Data.Services;
using LanguageBuilder.Services.Contracts;
using LanguageBuilder.Services.Models;
using LanguageBuilder.Services.Models.WordsSearch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LanguageBuilder.Services.Implementations
{
    public class WordsService : BaseRepository<Word, int>, IWordsService
    {
        private readonly LanguageBuilderDbContext _db;

        public WordsService(LanguageBuilderDbContext context)
            : base(context)
        {
            _db = context;
        }

        public async Task<Word> FindByIdAsync(int id)
        {
            return await _db
                .Words
                .Include(w => w.Language)
                .Include(w => w.SourceTranslations)
                .Include(w => w.TargetTranslations)
                .Where(w => w.Id == id && w.IsDeleted == false)
                .FirstOrDefaultAsync();
        }

        public IEnumerable<Word> GetByUser(User user)
        {
            return _db
                .UserWords
                .Where(uw => uw.UserId == user.Id)
                .Select(uw => uw.Word)
                .ToList();
        }

        public IEnumerable<Word> GetByUserId(string userId)
        {
            return _db
                .UserWords
                .Where(uw => uw.UserId == userId)
                .Select(uw => uw.Word)
                .ToList();
        }

        public IEnumerable<Word> GetByUserAndLanguage(User user, Language language)
        {
            return _db
                .UserWords
                .Where(uw => uw.UserId == user.Id && uw.Word.Language.Id == language.Id)
                .Select(uw => uw.Word)
                .ToList();
        }

        public async Task<IEnumerable<Word>> GetByUserAsync(User user)
        {
            return await this.GetByUserIdAsync(user.Id);
        }

        public async Task<IEnumerable<Word>> GetByUserIdAsync(string userId)
        {
            return await _db
                .UserWords
                .Where(uw => uw.UserId == userId)
                .Select(uw => uw.Word)
                .ToListAsync();
        }

        public async Task<Word> SoftDeleteAsync(int id)
        {
            var word = await _db
                .Words
                .Where(w => w.Id == id)
                .FirstOrDefaultAsync();

            if (word != null)
            {
                word.IsDeleted = true;
                await _db.SaveChangesAsync();
            }

            return word;
        }

        public async Task<bool> ExistInUserAsync(int id, string userId)
        {
            return await _db.UserWords.AnyAsync(uw => uw.WordId == id && uw.UserId == userId);
        }

        public async Task<UserWord> SoftDeleteInUserAsync(int id, string userId)
        {
            var wordInUser = await _db
                .UserWords
                .Where(uw => uw.WordId == id && uw.UserId == userId)
                .FirstOrDefaultAsync();

            if (wordInUser != null)
            {
                _db.Remove(wordInUser);
                await _db.SaveChangesAsync();
            }

            return wordInUser;
        }

        public async Task AddWordsWithTranslationWithUserConnectionAsync(Word source, Word target, string userId)
        {
            if (!await ExistAsync(source.Content)) { _db.Words.Add(source); }
            if (!await ExistAsync(target.Content)) { _db.Words.Add(target); }

            source = await _db.Words.Where(w => w.Content == source.Content).FirstOrDefaultAsync();
            target = await _db.Words.Where(w => w.Content == target.Content).FirstOrDefaultAsync();

            await _db
                .UserWords
                .AddAsync(
                    new UserWord
                    {
                        UserId = userId,
                        WordId = target.Id,
                        IsTargeted = true
                    });

            await _db
                .Translations
                .AddAsync(new Translation
                {
                    SourceWordId = source.Id,
                    TargetWordId = target.Id
                });

            _db.SaveChanges();
        }

        public async Task AddWordsWithTranslationAsync(Word source, Word target)
        {
            await _db
                .Words
                .AddRangeAsync(new[] { source, target });

            await _db
                .Translations
                .AddAsync(new Translation
                {
                    SourceWordId = source.Id,
                    TargetWordId = target.Id
                });

            _db.SaveChanges();
        }

        public async Task<IEnumerable<Word>> SearchAsync(string keywords, int languageId, int rows = 10)
        {
            return await _db
                .Words
                .Where(w => w.Content.Contains(keywords) && w.LanguageId == languageId)
                .Take(rows)
                .ToListAsync();
        }

        //public Task AddInUserAsync(Word word, User user)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<Response> Search(Request request, SortOptions sortOptions)
        {
            request.CancellationToken.ThrowIfCancellationRequested();

            int offset = (request.PageSize * request.PageNumber) - request.PageSize;
            if (offset < 0) { offset = 0; }

            var query = _db.Words.AsQueryable();

            switch (sortOptions.SortColumn.ToLower())
            {
                case "content":
                    query = sortOptions.SortDirection == SortDirection.Ascending
                        ? query.OrderBy(t => t.Content)
                        : query.OrderByDescending(t => t.Content);
                    break;
                case "gender":
                    query = sortOptions.SortDirection == SortDirection.Ascending
                        ? query.OrderBy(t => t.Gender)
                        : query.OrderByDescending(t => t.Gender);
                    break;
                default:
                    query = sortOptions.SortDirection == SortDirection.Ascending
                        ? query.OrderBy(t => t.Content)
                        : query.OrderByDescending(t => t.Content);
                    break;
            }

            if (request.Keywords != null)
            {
                query = query.Where(t => t.Content.Contains(request.Keywords.ToLower()));
            }

            if (request.Filter != null)
            {
                query = query.Where(request.Filter);
            }

            if (request.LanguageIds.Any())
            {
                query = query.Where(w => request.LanguageIds.Contains(w.LanguageId));
            }

            var totalItems = await query.CountAsync();

            query = query
                .Select(p => p)
                .Skip(offset)
                .Take(request.PageSize);

            var pagedResult = new PagedResult<Word>
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

        public async Task<bool> ExistAsync(string content)
        {
            return await _db.Words.AnyAsync(w => w.Content.ToLower() == content.ToLower());
        }
    }
}
