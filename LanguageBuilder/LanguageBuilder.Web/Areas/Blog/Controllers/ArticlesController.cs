using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageBuilder.Web;
using LanguageBuilder.Web.Infrastructure.Filters;

using static LanguageBuilder.Web.WebConstants;
using LanguageBuilder.Services.Blog;
using Microsoft.AspNetCore.Identity;
using LanguageBuilder.Services.Html;
using LanguageBuilder.Web.Areas.Blog.Models.Articles;
using LanguageBuilder.Web.Infrastructure.Extensions;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Web.Controllers;
using LanguageBuilder.Services.Contracts;

namespace LanguageBuilder.Web.Areas.Blog.Controllers
{
    [Area(BlogArea)]
    [Authorize(Roles = BlogAuthorRole)]
    public class ArticlesController : BaseController
    {
        private readonly IBlogArticleService _articles;
        private readonly UserManager<User> _userManager;
        private readonly IHtmlService _html;

        public ArticlesController(
            IBlogArticleService articles,
            UserManager<User> userManager,
            IUsersService userService, 
            IHtmlService html) : base(userService)
        {
            this._articles = articles;
            this._userManager = userManager;
            this._html = html;
        }

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
