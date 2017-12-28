using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static LanguageBuilder.Data.DataConstants;

namespace LanguageBuilder.Data.Models
{
    public class Word : BaseEntity<int>
    {
        [MaxLength(WORD_GENDER_MAX_LENGTH)]
        public string Gender { get; set; }

        [MaxLength(WORD_DEFINITION_MAX_LENGTH)]
        public string Definition { get; set; }

        [Required]
        [MinLength(WORD_CONTENT_MIN_LENGTH)]
        [MaxLength(WORD_CONTENT_MAX_LENGTH)]
        public string Content { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }

        public int? SyntaxTypeId { get; set; }
        public SyntaxType SyntaxType { get; set; }

        public bool IsDeleted { get; set; }

        public List<UserWord> Users { get; set; } = new List<UserWord>();

        public List<Example> Examples { get; set; } = new List<Example>();

        public List<Translation> SourceTranslations { get; set; } = new List<Translation>();
        public List<Translation> TargetTranslations { get; set; } = new List<Translation>();
    }
}
