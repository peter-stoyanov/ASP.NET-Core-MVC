using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static LanguageBuilder.Data.DataConstants;

namespace LanguageBuilder.Data.Models
{
    public class Example : BaseEntity<int>
    {
        public int WordId { get; set; }
        public Word Word { get; set; }

        public bool IsActive { get; set; }

        [Required]
        [MaxLength(EXAMPLE_MAX_LENGTH)]
        public string Content { get; set; }

        public List<UserWordExample> UserWords { get; set; } = new List<UserWordExample>();
    }
}
