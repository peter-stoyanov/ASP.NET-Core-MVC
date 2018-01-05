using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static LanguageBuilder.Data.DataConstants;

namespace LanguageBuilder.Data.Models
{
    public class SyntaxType : BaseEntity<int>
    {
        [Required]
        [MaxLength(SYNTAXTYPE_MAX_LENGTH)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public List<Word> Words { get; set; } = new List<Word>();
    }
}
