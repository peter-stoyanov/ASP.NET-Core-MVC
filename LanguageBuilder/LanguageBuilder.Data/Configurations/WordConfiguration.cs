using LanguageBuilder.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LanguageBuilder.Data.Configurations
{
    public class WordConfiguration : IEntityTypeConfiguration<Word>
    {
        public void Configure(EntityTypeBuilder<Word> builder)
        {
            builder
                .HasOne(w => w.Language)
                .WithMany(lan => lan.Words)
                .HasForeignKey(w => w.LanguageId);
                //.OnDelete(DeleteBehavior.Cascade);
        }
    }
}
