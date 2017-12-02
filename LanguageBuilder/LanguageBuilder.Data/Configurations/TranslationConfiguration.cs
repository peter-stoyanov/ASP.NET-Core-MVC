using LanguageBuilder.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LanguageBuilder.Data.Configurations
{
    public class TranslationConfiguration : IEntityTypeConfiguration<Translation>
    {
        public void Configure(EntityTypeBuilder<Translation> builder)
        {
            //builder
            //    .HasKey(t => new { t.SourceWordId, t.TargetWordId });

            builder
                .HasOne(t => t.SourceWord)
                .WithMany(w => w.SourceTranslations)
                .HasForeignKey(t => t.SourceWordId);

            builder
                .HasOne(t => t.TargetWord)
                .WithMany(w => w.TargetTranslations)
                .HasForeignKey(t => t.TargetWordId);
        }
    }
}
