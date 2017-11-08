using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LanguageBuilder.Data.Models
{
    public class Example
    {
        public int Id { get; set; }

        public int WordId { get; set; }
        public Word Word { get; set; }

        public bool IsActive { get; set; }

        [Required]
        [MaxLength(300)]
        public string Content { get; set; }

        public List<UserWordExample> UserWords { get; set; } = new List<UserWordExample>();

    }
}
