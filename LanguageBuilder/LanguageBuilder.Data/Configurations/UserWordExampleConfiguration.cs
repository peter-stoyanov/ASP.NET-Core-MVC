using LanguageBuilder.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LanguageBuilder.Data.Configurations
{
    public class UserWordExampleConfiguration : IEntityTypeConfiguration<UserWordExample>
    {
        public void Configure(EntityTypeBuilder<UserWordExample> builder)
        {
            builder
                .HasOne(uwe => uwe.Example)
                .WithMany(ex => ex.UserWords)
                .HasForeignKey(uwe => uwe.ExampleId);

            builder
                .HasOne(uwe => uwe.UserWord)
                .WithMany(uw => uw.Examples)
                .HasForeignKey(uwe => uwe.UserWordId);
        }
    }
}
