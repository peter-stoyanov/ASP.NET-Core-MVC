using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;
using LanguageBuilder.Web;
using LanguageBuilder.Web.Middleware;

namespace LanguageBuilder.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder builder)
            => builder.UseMiddleware<DatabaseMigrationMiddleware>();
    }
}
