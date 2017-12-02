﻿using LanguageBuilder.Data.Models;
using System.Collections.Generic;
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
    }
}
