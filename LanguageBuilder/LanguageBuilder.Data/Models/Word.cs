using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LanguageBuilder.Data.Models
{
    public class Word : BaseEntity<int>
    {
        //public int Id { get; set; }

        [MaxLength(15)]
        public string Gender { get; set; }

        [MaxLength(15)]
        public string SyntaxType { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(300)]
        public string Content { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }

        public bool IsDeleted { get; set; }

        public List<UserWord> Users { get; set; } = new List<UserWord>();

        public List<Example> Examples { get; set; } = new List<Example>();

        public List<Translation> SourceTranslations { get; set; } = new List<Translation>();
        public List<Translation> TargetTranslations { get; set; } = new List<Translation>();
    }
}
