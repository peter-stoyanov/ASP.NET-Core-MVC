using AutoMapper;
using LanguageBuilder.Data;
using LanguageBuilder.Web.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;
using System;

namespace LanguageBuilder.Tests
{
    public class Tests
    {
        private static bool _testsInitialized = false;

        public static void Initialize()
        {
            if (!_testsInitialized)
            {
                Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());

                _testsInitialized = true;
            }
        }

        public static LanguageBuilderDbContext GetDb()
        {
            var dbOptions = new DbContextOptionsBuilder<LanguageBuilderDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new LanguageBuilderDbContext(dbOptions);
        }
    }
}
