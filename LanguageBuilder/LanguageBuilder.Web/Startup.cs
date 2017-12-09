using LanguageBuilder.Data;
using LanguageBuilder.Data.Models;
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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LanguageBuilderDbContext>(options =>
                //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
                options.UseSqlServer(Configuration.GetConnectionString("AzureConnection")));

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

            // Add application services.
            services.AddDomainServices();

            services.AddAutoMapper();

            //services.AddEmbededFileProviderServices();
            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            services.AddMvc(config =>
            {
                config.Filters.Add(new ValidateModelStateAttributeAttribute());
                config.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });

            services.AddRouting(routing => routing.LowercaseUrls = true);

            services.AddSession(options =>
            {
                //options.Cookie.Name = ".LanguageBuilder.Session";
                //options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            // This one's not working yet...
            services.Configure<SessionOptions>(options =>
            {
                options.Cookie.HttpOnly = false;
                options.Cookie.Name = "LOGIN_COOKIE";
                options.Cookie.Domain = "asdf.com";
                options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
                options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.None;
            });

            services.Configure<CookiePolicyOptions>(o =>
            {
                o.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.None;
                o.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
                o.Secure = Microsoft.AspNetCore.Http.CookieSecurePolicy.None;
            });

            services
                .AddAuthentication("CookieAuth")
                .AddCookie("CookieAuth", options =>
                {
                    options.LoginPath = new PathString("/Home/Login/");
                    options.AccessDeniedPath = new PathString("/Home/Error/");
                    options.Cookie.HttpOnly = false;
                    options.Cookie.Name = "LOGIN_COOKIE";
                    options.Cookie.Domain = "asdf.com";
                    options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
                    options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.None;
                });

            services.AddCloudscribePagination();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, LanguageBuilderDbContext context)
        {
            //debug
            //env.EnvironmentName = "Production";

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseDatabaseMigration(context);

            //DbInitializer.Initialize(context);

            app
                .UseAuthentication()
                .UseCookiePolicy(new CookiePolicyOptions
                {
                    Secure = Microsoft.AspNetCore.Http.CookieSecurePolicy.None,
                    HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.None,
                    MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None,
                });

            app.UseSession();
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
        }
    }
}
