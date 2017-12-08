using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageBuilder.Web;

using static LanguageBuilder.Web.WebConstants;

namespace LanguageBuilder.Web.Areas.Blog.Controllers
{
    [Area(BlogArea)]
    [Authorize(Roles = BlogAuthorRole)]
    public class ArticlesController 
    {

    }
}
