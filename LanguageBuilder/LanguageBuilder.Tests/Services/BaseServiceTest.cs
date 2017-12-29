using LanguageBuilder.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageBuilder.Tests.Services
{
    public abstract class BaseServiceTest : BaseTest
    {
        protected BaseServiceTest()
        {
            TestStartup.Initialize();
        }

        protected LanguageBuilderDbContext InMemoryDatabase
        {
            get
            {
                DbContextOptions<LanguageBuilderDbContext> options = new DbContextOptionsBuilder<LanguageBuilderDbContext>()
                   .UseInMemoryDatabase(Guid.NewGuid().ToString())
                   .Options;

                return new LanguageBuilderDbContext(options);
            }
        }
    }
}
