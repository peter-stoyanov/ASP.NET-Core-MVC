using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LanguageBuilder.Data.Models
{
    public class SyntaxType : BaseEntity<int>
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public List<Word> Words { get; set; } = new List<Word>();
    }
}
