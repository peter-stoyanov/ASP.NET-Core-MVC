using cloudscribe.Pagination.Models;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Models.WordsSearch;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace LanguageBuilder.Services.Contracts
{
    public interface IWordsService : IRepository<Word, int>, IAsyncRepository<Word, int>
    {
        IEnumerable<Word> GetByUser(User user);

        Task<IEnumerable<Word>> GetByUserAsync(User user);

        Task<IEnumerable<Word>> GetByUserIdAsync(string userId);

        IEnumerable<Word> GetByUserAndLanguage(User user, Language language);

        Task<Word> SoftDeleteAsync(int id);

        Task<bool> ExistInUserAsync(int id, string userId);

        Task<UserWord> SoftDeleteInUserAsync(int id, string userId);

        Task AddWordsWithTranslationAsync(Word source, Word target, string userId);

        Task<IEnumerable<Word>> SearchAsync(string keywords, int rows = 10);

        Task AddInUserAsync(Word word, User user);

        Task<Response> Search(Request request, Expression<Func<Word, object>> sortColumnSelector = null, Expression<Func<Word, bool>> criteria = null);
    }
}
