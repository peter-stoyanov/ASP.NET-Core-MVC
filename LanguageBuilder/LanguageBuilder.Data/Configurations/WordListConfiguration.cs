using LanguageBuilder.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LanguageBuilder.Data.Configurations
{
    public class WordListConfiguration : IEntityTypeConfiguration<WordList>
    {
        public void Configure(EntityTypeBuilder<WordList> builder)
        {
            builder
                .HasMany(list => list.Words)
                .WithOne(uw => uw.WordList)
                .HasForeignKey(uw => uw.WordListId);
        }
    }
}
