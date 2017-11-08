using LanguageBuilder.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageBuilder.Web.Middleware
{
    public class DatabaseMigrationMiddleware
    {
        private readonly RequestDelegate next;

        public DatabaseMigrationMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(HttpContext context)
        {
            context.RequestServices.GetRequiredService<LanguageBuilderDbContext>().Database.Migrate();

            return this.next(context);
        }
    }
}
