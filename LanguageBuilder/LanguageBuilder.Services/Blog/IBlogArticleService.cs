﻿using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Blog.Models;
using LanguageBuilder.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageBuilder.Services.Blog
{
    public interface IBlogArticleService : IRepository<Article, int>, IAsyncRepository<Article, int>
    {
        Task<IEnumerable<BlogArticleListingServiceModel>> AllAsync(int page = 1);

        Task<IEnumerable<BlogArticleListingServiceModel>> ByAuthorIdAsync(string authorId, int page = 1);

        Task<int> TotalAsync();

        Task<BlogArticleDetailsServiceModel> ById(int id);

        Task CreateAsync(string title, string content, string authorId);
    }
}
