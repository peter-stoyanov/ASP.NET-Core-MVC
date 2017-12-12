using AutoMapper;
using LanguageBuilder.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LanguageBuilder.Web.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            Assembly
                .GetAssembly(typeof(IService))
                .GetTypes()
                .Where(t => t.IsClass && t.GetInterfaces().Any(i => i.Name == $"I{t.Name}"))
                .Select(t => new
                {
                    Interface = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .ToList()
                .ForEach(s => services.AddTransient(s.Interface, s.Implementation));

            return services;
        }

        //public static IServiceCollection AddEmbededFileProviderServices(this IServiceCollection services)
        //{
        //    var assembly = typeof(ComponentLibrary.BootstrapModalComponent).GetTypeInfo().Assembly;

        //    var embededFileProvider = new EmbeddedFileProvider(assembly, "ComponentLibrary");

        //    services.Configure<RazorViewEngineOptions>(options =>
        //    {
        //        options.FileProviders.Add(embededFileProvider);
        //        //options.PageViewLocationFormats
        //    });

        //    return services;
        //}

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(DependencyContext.Default);

            return services;
        }

        private static IServiceCollection AddAutoMapper(
            this IServiceCollection services,
            DependencyContext dependencyContext)
        {
            services.AddAutoMapper(dependencyContext.RuntimeLibraries
                .SelectMany(lib => lib.GetDefaultAssemblyNames(dependencyContext).Select(Assembly.Load)));

            return services;
        }

        private static IServiceCollection AddAutoMapper(
            this IServiceCollection services,
            IEnumerable<Assembly> assembliesToScan)
        {
            assembliesToScan = assembliesToScan as Assembly[] ?? assembliesToScan.ToArray();

            var allTypes = assembliesToScan.SelectMany(a => a.ExportedTypes).ToArray();

            var profiles = allTypes
                .Where(t => typeof(Profile).GetTypeInfo().IsAssignableFrom(t.GetTypeInfo())
                            && !t.GetTypeInfo().IsAbstract);

            Mapper.Initialize(cfg =>
            {
                cfg.AllowNullDestinationValues = true;

                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });

            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(sp =>
                new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));

            return services;
        }
    }
}
