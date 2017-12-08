using LanguageBuilder.Data.Configurations;
using LanguageBuilder.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LanguageBuilder.Data
{
    public class LanguageBuilderDbContext : IdentityDbContext<User, Role, string>
    {
        public DbSet<Word> Words { get; set; }
        public DbSet<Example> Examples { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Subscription> SubscriptionTypes { get; set; }
        public DbSet<Translation> Translations { get; set; }
        public DbSet<UserLanguage> UserLanguages { get; set; }
        public DbSet<UserWord> UserWords { get; set; }
        public DbSet<UserWordExample> UserWordExamples { get; set; }
        public DbSet<WordList> WordLists { get; set; }
        public DbSet<SyntaxType> SyntaxTypes { get; set; }

        public LanguageBuilderDbContext(DbContextOptions<LanguageBuilderDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ExampleConfiguration());
            builder.ApplyConfiguration(new LanguageConfiguration());
            builder.ApplyConfiguration(new SubscriptionConfiguration());
            builder.ApplyConfiguration(new TranslationConfiguration());
            builder.ApplyConfiguration(new UserLanguageConfiguration());
            builder.ApplyConfiguration(new UserWordConfiguration());
            builder.ApplyConfiguration(new UserWordExampleConfiguration());
            builder.ApplyConfiguration(new WordConfiguration());
            builder.ApplyConfiguration(new WordListConfiguration());
            builder.ApplyConfiguration(new SyntaxTypeConfiguration());

            SetupTableNamesAndCascadeDelete(builder);
        }

        private static void SetupTableNamesAndCascadeDelete(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.Relational().TableName = "tbl_" + entity.ClrType.Name;

                foreach (var fk in entity.GetForeignKeys())
                {
                    fk.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }
        }

        // enforces data validation with attributes before saving changes to Db
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var serviceProvider = this.GetService<IServiceProvider>();
            var items = new Dictionary<object, object>();

            foreach (var entry in this.ChangeTracker.Entries().Where(e => (e.State == EntityState.Added) || (e.State == EntityState.Modified)))
            {
                var entity = entry.Entity;
                var context = new ValidationContext(entity, serviceProvider, items);
                var results = new List<ValidationResult>();

                if (Validator.TryValidateObject(entity, context, results, true) == false)
                {
                    foreach (var result in results)
                    {
                        if (result != ValidationResult.Success)
                        {
                            throw new ValidationException(result.ErrorMessage);
                        }
                    }
                }
            }

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
    }
}
