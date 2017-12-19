using LanguageBuilder.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LanguageBuilder.Data.Configurations
{
    public class SyntaxTypeConfiguration : IEntityTypeConfiguration<SyntaxType>
    {
        public void Configure(EntityTypeBuilder<SyntaxType> builder)
        {
            builder
                .HasMany(s => s.Words)
                .WithOne(w => w.SyntaxType)
                .HasForeignKey(w => w.SyntaxTypeId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
