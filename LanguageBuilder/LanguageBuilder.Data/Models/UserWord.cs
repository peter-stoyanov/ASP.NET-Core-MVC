using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LanguageBuilder.Data.Models
{
    public class UserWord
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int WordId { get; set; }
        public Word Word { get; set; }

        public int StudyLevel { get; set; }
        public int MatchLevel { get; set; }
        public int ReproduceLevel { get; set; }

        public bool IsTargeted { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime NextReview { get; set; }

        [MaxLength(300)]
        public string Notes { get; set; }

        public int? WordListId { get; set; }
        public WordList WordList { get; set; }

        public List<UserWordExample> Examples { get; set; } = new List<UserWordExample>();
    }
}
