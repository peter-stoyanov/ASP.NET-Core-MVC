using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static LanguageBuilder.Data.DataConstants;

namespace LanguageBuilder.Web.Areas.Blog.Models.Articles
{
    public class ArticleEditViewModel : ArticleBaseViewModel
    {
        public int Id { get; set; }
    }
}
