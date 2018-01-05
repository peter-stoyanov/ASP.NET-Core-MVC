using System;
using System.ComponentModel.DataAnnotations;

using static LanguageBuilder.Data.DataConstants;

namespace LanguageBuilder.Data.Models
{
    public class Article : BaseEntity<int>
    {
        [Required]
        [MinLength(ARTICLE_TITLE_MINLENGTH)]
        [MaxLength(ARTICLE_TITLE_MAXLENGTH)]
        public string Title { get; set; }

        [Required]
        [MinLength(ARTICLE_CONTENT_MIN_LENGTH)]
        [MaxLength(ARTICLE_CONTENT_MAX_LENGTH)]
        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public User Author { get; set; }
    }
}
