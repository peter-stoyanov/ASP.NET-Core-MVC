using LanguageBuilder.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LanguageBuilder.Data
{
    public static class DbInitializer
    {
        public static void Initialize(LanguageBuilderDbContext context)
        {

            context.Database.EnsureCreated();

            if (context.SubscriptionTypes.Any())
            {
                return;   // DB has been seeded
            }

            SeedSubscriprionTypes(context);

            context.SaveChanges();
        }

        private static void SeedSubscriprionTypes(LanguageBuilderDbContext context)
        {
            var subscriptions = new List<Subscription>()
            {
                new Subscription() { Name = "free" },
                new Subscription() { Name = "standard" },
                new Subscription() { Name = "premium" }
            };

            context.SubscriptionTypes.AddRange(subscriptions);

            context.SaveChanges();
        }
    }
}
