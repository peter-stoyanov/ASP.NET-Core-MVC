using LanguageBuilder.Data;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LanguageBuilder.Tests.Web
{
    public class BaseWebTest : BaseTest
    {
        protected BaseWebTest()
        {
            TestStartup.Initialize();
        }

        protected LanguageBuilderDbContext Database
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

