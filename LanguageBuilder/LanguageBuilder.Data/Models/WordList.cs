using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static LanguageBuilder.Data.DataConstants;

namespace LanguageBuilder.Data.Models
{
    public class WordList : BaseEntity<int>
    {
        [Required]
        [MaxLength(WORDLIST_NAME_MAX_LENGTH)]
        public string Name { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsActive { get; set; }

        [MaxLength(WORDLIST_NOTES_MAX_LENGTH)]
        public string Notes { get; set; }

        public List<UserWord> Words { get; set; } = new List<UserWord>();
    }
}
