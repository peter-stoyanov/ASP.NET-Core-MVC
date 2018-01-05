using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static LanguageBuilder.Data.DataConstants;

namespace LanguageBuilder.Data.Models
{
    public class UserWord : BaseEntity<int>
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public int WordId { get; set; }
        public Word Word { get; set; }

        public int StudyLevel { get; set; } = 1;
        public int MatchLevel { get; set; } = 1;
        public int ReproduceLevel { get; set; } = 1;

        public bool IsTargeted { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public DateTime NextReview { get; set; } = DateTime.Now.AddDays(2);

        [MaxLength(WORD_NOTES_MAX_LENGTH)]
        public string Notes { get; set; }

        public int? WordListId { get; set; }
        public WordList WordList { get; set; }

        public List<UserWordExample> Examples { get; set; } = new List<UserWordExample>();
    }
}
