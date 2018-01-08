using LanguageBuilder.Services.Blog;
using LanguageBuilder.Services.Contracts;
using LanguageBuilder.Services.Html;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Web.Http;

namespace LanguageBuilder.Web.Controllers
{
    [AllowAnonymous]
    public class RssController : BaseAnonymousController
    {
        private readonly IBlogArticleService _articles;
        private readonly IHtmlService _html;

        public RssController(
            IBlogArticleService articles,
            IUsersService userService,
            IHtmlService html) : base(userService)
        {
            this._articles = articles;
            this._html = html;
        }

        public async Task<IActionResult> Feed(string type)
        {
            var model = await this._articles.AllAsync();

            return View(model);
        }
    }
}
