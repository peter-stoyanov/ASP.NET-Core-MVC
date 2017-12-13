using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LanguageBuilder.Data.Models
{
    public class Language : BaseEntity<int>
    {
        [Required]
        [MinLength(1)]
        [MaxLength(30)]
        public string Name { get; set; }

        public bool IsUsed { get; set; } = true;

        public List<UserLanguage> Users { get; set; } = new List<UserLanguage>();

        public List<Word> Words { get; set; } = new List<Word>();
    }
}
