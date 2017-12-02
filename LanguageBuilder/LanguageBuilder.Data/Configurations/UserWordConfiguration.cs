using LanguageBuilder.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LanguageBuilder.Data.Configurations
{
    public class UserWordConfiguration : IEntityTypeConfiguration<UserWord>
    {
        public void Configure(EntityTypeBuilder<UserWord> builder)
        {
            //builder
            //    .HasKey(uw => new { uw.UserId, uw.WordId});

            builder
                .HasOne(uw => uw.User)
                .WithMany(u => u.Words)
                .HasForeignKey(uw => uw.UserId);

            builder
                .HasOne(uw => uw.Word)
                .WithMany(w => w.Users)
                .HasForeignKey(uw => uw.WordId);
        }
    }
}
