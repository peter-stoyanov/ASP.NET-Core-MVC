using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static LanguageBuilder.Data.DataConstants;

namespace LanguageBuilder.Data.Models
{
    public class Article : BaseEntity<int>
    {
        [Required]
        [MinLength(ArticleTitleMinLength)]
        [MaxLength(ArticleTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(ArticleContentMinLength)]
        [MaxLength(ArticleContentMaxLength)]
        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        [Required]
        public string AuthorId { get; set; }
        public User Author { get; set; }
    }
}
