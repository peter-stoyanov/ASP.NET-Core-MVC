﻿using LanguageBuilder.Services;
using LanguageBuilder.Services.Blog.Models;
using System;
using System.Collections.Generic;

namespace LanguageBuilder.Web.Areas.Blog.Models.Articles
{
    public class ArticleListingViewModel
    {
        public IEnumerable<BlogArticleListingServiceModel> Articles { get; set; }

        public int TotalArticles { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)this.TotalArticles / ServiceConstants.BLOGARTICLES_PAGESIZE);

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage <= 1 ? 1 : this.CurrentPage - 1;

        public int NextPage
            => this.CurrentPage == this.TotalPages
                ? this.TotalPages
                : this.CurrentPage + 1;
    }
}
