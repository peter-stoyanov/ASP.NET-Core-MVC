using LanguageBuilder.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LanguageBuilder.Data.Configurations
{
    public class TranslationConfiguration : IEntityTypeConfiguration<Translation>
    {
        public void Configure(EntityTypeBuilder<Translation> builder)
        {
            builder
                .HasOne(t => t.SourceWord)
                .WithMany(w => w.SourceTranslations)
                .HasForeignKey(t => t.SourceWordId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(t => t.TargetWord)
                .WithMany(w => w.TargetTranslations)
                .HasForeignKey(t => t.TargetWordId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
