using LanguageBuilder.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LanguageBuilder.Data.Configurations
{
    public class UserSubscriptionConfiguration : IEntityTypeConfiguration<UserSubscription>
    {
        public void Configure(EntityTypeBuilder<UserSubscription> builder)
        {
            builder
                .HasKey(us => new { us.UserId, us.SubscriptionId });

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
