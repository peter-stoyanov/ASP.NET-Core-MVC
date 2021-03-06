﻿using Hangfire;
using LanguageBuilder.Data;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Web.Hubs;
using LanguageBuilder.Web.Infrastructure.Extensions;
using LanguageBuilder.Web.Infrastructure.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace LanguageBuilder.Web
{
    public class Startup
    {
        private string _facebookAppId = null;
        private string _facebookAppSecret = null;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // load tokens from secret manager
            _facebookAppId = Configuration["Authentication:Facebook:AppId"];
            _facebookAppSecret = Configuration["Authentication:Facebook:AppSecret"];

            services.AddDbContext<LanguageBuilderDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddHangfire(x => 
                x.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));

            services
                .AddIdentity<User, Role>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 3;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<LanguageBuilderDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            });

            services.AddDomainServices();

            services.AddAutoMapper();

            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            services.AddMvc(config =>
            {
                config.Filters.Add(new ValidateModelStateAttributeAttribute());
                config.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
                config.SslPort = 44321;
                config.Filters.Add(new RequireHttpsAttribute());
            });

            services.AddAntiforgery(
                options =>
                {
                    options.Cookie.Name = "_af";
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.HeaderName = "X-XSRF-TOKEN";
                }
            );

            services.AddCors();

            services.AddRouting(routing => routing.LowercaseUrls = true);

            services.AddCloudscribePagination();

            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            IApplicationLifetime lifetime,
            LanguageBuilderDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseDatabaseMigration(context);

            app
                .UseAuthentication()
                .UseCookiePolicy(new CookiePolicyOptions
                {
                    Secure = Microsoft.AspNetCore.Http.CookieSecurePolicy.None,
                    HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.None,
                    MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None,
                });

            app.UseHangfireServer();
            app.UseHangfireDashboard();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "blog",
                    template: "blog/articles/{action}/{id}/{title}",
                    defaults: new { area = "Blog", controller = "Articles", action = "Index" });

                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseCors(
                options => options.WithOrigins("*").AllowAnyMethod()
            );

            app.UseSignalR(routes =>
            {
                routes.MapHub<NotificationsHub>("notificationsHub");
            });
        }
    }
}
