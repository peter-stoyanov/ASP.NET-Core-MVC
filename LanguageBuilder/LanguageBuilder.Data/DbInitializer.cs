using LanguageBuilder.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LanguageBuilder.Data
{
    public static class DbInitializer
    {
        private static LanguageBuilderDbContext _context;

        public static void Initialize(LanguageBuilderDbContext context)
        {
            _context = context;

            _context.Database.EnsureCreated();

            //if (context.SubscriptionTypes.Any())
            //{
            //    return;   // DB has been seeded
            //}

            //SeedSubscriprionTypes(context);
            //SeedLanguages();
            //SeedWords();
            //SeedUserWords();

            context.SaveChanges();
        }

        private static void SeedUserWords()
        {
            var firstUser = _context.Users.FirstOrDefault();

            var userWords = new List<UserWord>()
            {
                new UserWord()
                {
                    CreatedOn = DateTime.Now,
                    IsTargeted = true,
                    MatchLevel = 1,
                    NextReview = DateTime.Now.AddDays(3),
                    ReproduceLevel = 1,
                    StudyLevel = 1,
                    UserId = firstUser.Id,
                    WordId = 1
                },
            };

            _context.UserWords.AddRange(userWords);
        }

        private static void SeedLanguages()
        {
            var languages = new List<Language>()
            {
                new Language() { Name = "English" },
                new Language() { Name = "German" }
            };

            _context.Languages.AddRange(languages);
        }

        private static void SeedWords()
        {
            var words = new List<Word>()
            {
                new Word() { Content = "Haus", Gender = "das", LanguageId = 2 }
            };

            _context.Words.AddRange(words);
        }

        private static void SeedSubscriprionTypes()
        {
            var subscriptions = new List<Subscription>()
            {
                new Subscription() { Name = "free" },
                new Subscription() { Name = "standard" },
                new Subscription() { Name = "premium" }
            };

            _context.SubscriptionTypes.AddRange(subscriptions);
        }
    }
}
