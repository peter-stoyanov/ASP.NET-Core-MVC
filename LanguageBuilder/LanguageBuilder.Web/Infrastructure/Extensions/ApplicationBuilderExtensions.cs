﻿using LanguageBuilder.Data;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Contracts;
using LanguageBuilder.Web.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageBuilder.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        //public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder builder)
        //    => builder.UseMiddleware<DatabaseMigrationMiddleware>();

        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app, LanguageBuilderDbContext context)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<LanguageBuilderDbContext>().Database.Migrate();

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<Role>>();
                var languageService = serviceScope.ServiceProvider.GetService<ILanguageService>();
                var subscriptionService = serviceScope.ServiceProvider.GetService<ISubscriptionService>();

                Task
                    .Run(async () =>
                    {
                        // feed roles
                        var adminRole = WebConstants.AdministratorRole;
                        var roles = new[]
                        {
                            adminRole,
                            WebConstants.UserRole,
                            WebConstants.BlogAuthorRole
                        };

                        foreach (var role in roles)
                        {
                            var roleExists = await roleManager.RoleExistsAsync(role);

                            if (!roleExists)
                            {
                                await roleManager.CreateAsync(new Role
                                {
                                    Name = role
                                });
                            }
                        }

                        // seed subscription types
                        var subscriptionCount = await (await subscriptionService.GetAllAsync()).CountAsync();
                        if (subscriptionCount == 0)
                        {
                            var subscriptions = new List<Subscription>()
                            {
                                new Subscription() { Name = "free" },
                                new Subscription() { Name = "standard" },
                                new Subscription() { Name = "premium" }
                            };

                            context.SubscriptionTypes.AddRange(subscriptions);
                        }

                        // insert admin user
                        var adminEmail = "admin@languagebuilder.com";
                        var adminUser = await userManager.FindByEmailAsync(adminEmail);

                        if (adminUser == null)
                        {
                            var user = new User { UserName = adminEmail, Email = adminEmail, SubscriptionId = 1 };

                            await userManager.CreateAsync(user, "admin123456");
                            await userManager.AddToRolesAsync(user, new[] { adminRole, WebConstants.UserRole, WebConstants.BlogAuthorRole });
                        }

                        // seed languages
                        var languageCount = await (await languageService.GetAllAsync()).CountAsync();
                        if (languageCount == 0)
                        {
                            var languages = new List<Language>()
                            {
                                new Language() { Name = "English" },
                                new Language() { Name = "German" }
                            };

                            context.Languages.AddRange(languages);
                        }

                    })
                    .Wait();
            }

            return app;
        }
    }
}
