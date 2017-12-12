using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace LanguageBuilder.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<LanguageBuilderDbContext>
    {
        public LanguageBuilderDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<LanguageBuilderDbContext>();

            var connectionString = configuration.GetConnectionString("AzureConnection");

            builder.UseSqlServer(connectionString);

            return new LanguageBuilderDbContext(builder.Options);
        }
    }
}
