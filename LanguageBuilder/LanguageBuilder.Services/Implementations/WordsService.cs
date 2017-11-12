﻿using System;
using System.Collections.Generic;
using System.Text;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Data;
using LanguageBuilder.Services.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LanguageBuilder.Services.Implementations
{
    public class WordsService : IWordsService
    {
        private readonly LanguageBuilderDbContext _db;

        public WordsService(LanguageBuilderDbContext context)
        {
            _db = context;
        }

        public void Add(Word word)
        {
            _db.Words.Add(word);
            _db.SaveChanges();
        }

        public async Task AddAsync(Word word)
        {
            // todo: should this be done like that ?
            await _db.Words.AddAsync(word);
            await _db.SaveChangesAsync();
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

        public async Task<bool> ExistAsync(int id)
        {
            return await _db.Words.AnyAsync(w => w.Id == id);
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
        
    }
}
