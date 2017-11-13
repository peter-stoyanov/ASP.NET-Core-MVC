using LanguageBuilder.Data.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageBuilder.Services.Contracts
{
    public interface IWordsService
    {
        IEnumerable<Word> GetByUser(User user);
        Task<IEnumerable<Word>> GetByUserAsync(User user);
        Task<IEnumerable<Word>> GetByUserIdAsync(string userId);
        IEnumerable<Word> GetByUserAndLanguage(User user, Language language);
        void Add(Word word);
        Task AddAsync(Word word);
        Task<Word> FindByIdAsync(int id);
        Task<bool> ExistAsync(int id);
        Task<Word> SoftDeleteAsync(int id);
        Task<bool> ExistInUserAsync(int id, string userId);
        Task<UserWord> SoftDeleteInUserAsync(int id, string userId);
        Task AddWordsWithTranslation(Word source, Word target, string userId);
        Task<IEnumerable<Word>> SearchAsync(string keywords, int rows = 10);
    }
}
