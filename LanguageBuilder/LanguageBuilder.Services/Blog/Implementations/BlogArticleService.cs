using AutoMapper.QueryableExtensions;
using LanguageBuilder.Data;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Data.Services;
using LanguageBuilder.Services.Blog.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static LanguageBuilder.Services.ServiceConstants;

namespace LanguageBuilder.Services.Blog.Implementations
{
    public class BlogArticleService : BaseRepository<Article, int>, IBlogArticleService
    {
        private readonly LanguageBuilderDbContext db;

        public BlogArticleService(LanguageBuilderDbContext db)
            : base(db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<BlogArticleListingServiceModel>> AllAsync(int page = 1)
            => await this.db
                .Articles
                .OrderByDescending(a => a.PublishDate)
                .Skip((page - 1) * BlogArticlesPageSize)
                .Take(BlogArticlesPageSize)
                .ProjectTo<BlogArticleListingServiceModel>()
                .ToListAsync();

        public async Task<IEnumerable<BlogArticleListingServiceModel>> ByAuthorIdAsync(string authorId, int page = 1)
            => await this.db
                .Articles
                .Where(a => a.AuthorId == authorId)
                .OrderByDescending(a => a.PublishDate)
                .Skip((page - 1) * BlogArticlesPageSize)
                .Take(BlogArticlesPageSize)
                .ProjectTo<BlogArticleListingServiceModel>()
                .ToListAsync();

        public async Task<int> TotalAsync()
            => await this.db.Articles.CountAsync();

        public async Task<BlogArticleDetailsServiceModel> ById(int id)
            => await this.db
                .Articles
                .Where(a => a.Id == id)
                .ProjectTo<BlogArticleDetailsServiceModel>()
                .FirstOrDefaultAsync();

        public async Task CreateAsync(string title, string content, string authorId)
        {
            var article = new Article
            {
                Title = title,
                Content = content,
                PublishDate = DateTime.UtcNow,
                AuthorId = authorId
            };

            this.db.Add(article);

            await this.db.SaveChangesAsync();
        }
    }
}
