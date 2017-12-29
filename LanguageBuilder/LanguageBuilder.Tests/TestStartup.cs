using AutoMapper;
using LanguageBuilder.Data;
using LanguageBuilder.Web.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;
using System;

namespace LanguageBuilder.Tests
{
    public class TestStartup
    {
        private static object sync = new object();
        private static bool testsInitialized = false;

        public static void Initialize()
        {
            lock (sync)
            {
                if (!testsInitialized)
                {
                    Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());
                    testsInitialized = true;
                }
            }
        }

        //public static LanguageBuilderDbContext GetDb()
        //{
        //    var dbOptions = new DbContextOptionsBuilder<LanguageBuilderDbContext>()
        //        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        //        .Options;

        //    return new LanguageBuilderDbContext(dbOptions);
        //}
    }
}
