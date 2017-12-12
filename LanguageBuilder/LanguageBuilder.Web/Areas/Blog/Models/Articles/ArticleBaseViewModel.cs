using System.ComponentModel.DataAnnotations;
using static LanguageBuilder.Data.DataConstants;

namespace LanguageBuilder.Web.Areas.Blog.Models.Articles
{
    public class ArticleBaseViewModel
    {
        [Required]
        [MinLength(ArticleTitleMinLength)]
        [MaxLength(ArticleTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
