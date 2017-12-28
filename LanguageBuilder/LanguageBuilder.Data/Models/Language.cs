using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static LanguageBuilder.Data.DataConstants;

namespace LanguageBuilder.Data.Models
{
    public class Language : BaseEntity<int>
    {
        [Required]
        [MinLength(LANGUAGE_MIN_LENGTH)]
        [MaxLength(LANGUAGE_MAX_LENGTH)]
        public string Name { get; set; }

        public bool IsUsed { get; set; } = true;

        public List<UserLanguage> Users { get; set; } = new List<UserLanguage>();

        public List<Word> Words { get; set; } = new List<Word>();
    }
}
