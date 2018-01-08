using LanguageBuilder.Services.Blog;
using LanguageBuilder.Services.Contracts;
using LanguageBuilder.Services.Html;
using LanguageBuilder.Web.Areas.Blog.Models.Articles;
using LanguageBuilder.Web.Controllers;
using LanguageBuilder.Web.Infrastructure.Extensions;
using LanguageBuilder.Web.Infrastructure.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static LanguageBuilder.Web.WebConstants;

namespace LanguageBuilder.Web.Areas.Blog.Controllers
{
    [Area(BLOG_AREA)]
    [Authorize(Roles = BLOGAUTHOR_ROLE)]
    public class ArticlesController : BaseController
    {
        private readonly IBlogArticleService _articles;
        private readonly IHtmlService _html;

        public ArticlesController(
            IBlogArticleService articles,
            IUsersService userService,
            IHtmlService html)
            : base(userService)
        {
            this._articles = articles;
            this._html = html;
        }

        public async Task<IActionResult> My(int page = 1)
            => View(new ArticleListingViewModel
            {
                Articles = await this._articles.ByAuthorIdAsync(LoggedUser.Id),
                TotalArticles = await this._articles.TotalAsync(),
                CurrentPage = page
            });

        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1)
            => View(new ArticleListingViewModel
            {
                Articles = await this._articles.AllAsync(page),
                TotalArticles = await this._articles.TotalAsync(),
                CurrentPage = page
            });

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
            => this.ViewOrNotFound(await this._articles.ById(id));

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateModelStateAttribute]
        public async Task<IActionResult> Create(ArticleCreateViewModel model)
        {
            model.Content = this._html.Sanitize(model.Content);

            await this._articles.CreateAsync(model.Title, model.Content, this.LoggedUser.Id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var article = await this._articles.GetAsync(id);

            if (article.AuthorId != LoggedUser.Id)
            {
                Unauthorized();
            }

            if (article == null)
            {
                NotFound();
            }

            var model = new ArticleCreateViewModel
            {
                Title = article.Title,
                Content = article.Content
            };

            return View(model);
        }

        [HttpPost]
        [ValidateModelStateAttribute]
        public async Task<IActionResult> Edit(ArticleEditViewModel model)
        {
            var article = await _articles.GetAsync(model.Id);

            if (article == null)
            {
                NotFound();
            }

            article.Title = model.Title;
            article.Content = this._html.Sanitize(model.Content);

            await this._articles.UpdateAsync(article);

            return RedirectToAction(nameof(Index));
        }
    }
}
