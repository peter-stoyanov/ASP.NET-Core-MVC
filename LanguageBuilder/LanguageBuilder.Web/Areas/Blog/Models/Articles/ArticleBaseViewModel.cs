using System.ComponentModel.DataAnnotations;
using static LanguageBuilder.Data.DataConstants;

namespace LanguageBuilder.Web.Areas.Blog.Models.Articles
{
    public class ArticleBaseViewModel
    {
        [Required]
        [MinLength(ARTICLE_TITLE_MINLENGTH)]
        [MaxLength(ARTICLE_TITLE_MAXLENGTH)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
