using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LanguageBuilder.Data.Models
{
    public class WordList : BaseEntity<int>
    {
        //public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsActive { get; set; }

        [MaxLength(300)]
        public string Notes { get; set; }

        public List<UserWord> Words { get; set; } = new List<UserWord>();
    }
}
