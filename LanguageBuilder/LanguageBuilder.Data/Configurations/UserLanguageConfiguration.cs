using LanguageBuilder.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LanguageBuilder.Data.Configurations
{
    public class UserLanguageConfiguration : IEntityTypeConfiguration<UserLanguage>
    {
        public void Configure(EntityTypeBuilder<UserLanguage> builder)
        {
            //builder
            //    .HasKey(ul => new { ul.UserId, ul.LanguageId });

            builder
                .HasOne(ul => ul.User)
                .WithMany(u => u.Languages)
                .HasForeignKey(ul => ul.UserId);

            builder
                .HasOne(ul => ul.Language)
                .WithMany(lan => lan.Users)
                .HasForeignKey(ul => ul.LanguageId);
        }
    }
}
