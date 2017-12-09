using cloudscribe.Pagination.Models;
using LanguageBuilder.Data;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Contracts;
using LanguageBuilder.Services.Models.UsersSearch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LanguageBuilder.Services.Implementations
{
    public class UsersService :  IUsersService
    {
        private readonly LanguageBuilderDbContext _db;

        public UsersService(LanguageBuilderDbContext context)
        {
            _db = context;
        }

        public User GetByIdentity(string identityName)
        {
            return _db
                .Users
                .Where(u => u.UserName == identityName)
                .FirstOrDefault();
        }

        public async Task<User> GetByIdentityAsync(string identityName)
        {
            return await _db
                .Users
                .Where(u => u.UserName == identityName)
                .FirstOrDefaultAsync();
        }

        public User GetById(string id)
        {
            return _db
                .Users
                .Where(u => u.Id == id)
                .FirstOrDefault();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _db
                .Users
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Response> Search(
            Request request,
            Expression<Func<User, object>> sortColumnSelector = null)
        {
            request.CancellationToken.ThrowIfCancellationRequested();
            Expression<Func<User, bool>> criteria = u => true;

            int offset = (request.PageSize * request.PageNumber) - request.PageSize;
            if (offset < 0) { offset = 0; }

            string keyword = request.Keywords?.ToLower();

            if (keyword != null) { criteria = u => u.Email.Contains(keyword) || u.Name.Contains(keyword) || u.UserName.Contains(keyword); }

            //var users = _db.Users
            //    .OrderBy(sortColumnSelector)
            //    .AsQueryable();

            //if (keyword != null)
            //{
            //    users = users.Where(u => u.Email.Contains(keyword) || u.Name.Contains(keyword) || u.UserName.Contains(keyword));
            //};

            var query = _db.Users
                .OrderBy(sortColumnSelector)
                .Where(criteria)
                .Select(p => p)
                .Skip(offset)
                .Take(request.PageSize);

            var pagedResult = new PagedResult<User>
            {
                Data = await query.AsNoTracking().ToListAsync(request.CancellationToken),
                TotalItems = await _db.Users.CountAsync(),
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            var response = new Response()
            {
                Records = pagedResult
            };

            return response;

        }
    }
}
