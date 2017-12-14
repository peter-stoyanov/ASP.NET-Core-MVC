using Xunit;
using Xunit.Sdk;
using LanguageBuilder.Web.Controllers;
using Moq;
using LanguageBuilder.Services.Contracts;
using LanguageBuilder.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using LanguageBuilder.Data;
using System;

namespace LanguageBuilder.Tests.Web
{
    public class WordsControllerTests
    {
        private readonly IServiceProvider _serviceProvider;
        //private readonly IWordsService _mockWordsService;
        //private readonly ILanguageService _mockLanguageService;

        // create a constructor to configure services
        public WordsControllerTests()
        {
            var services = new ServiceCollection();

            services
            .AddEntityFrameworkInMemoryDatabase()
            .AddDbContext<LanguageBuilderDbContext>();

            _serviceProvider = services.BuildServiceProvider();

            //_mockWordsService = new Mock<IWordsService>().Object;
        }
        
        [Fact]
        public void Index_ReturnsView()
        {
            // Arrange
            var dbContext = _serviceProvider.GetRequiredService<LanguageBuilderDbContext>();
            var controller = new HomeController();

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
            Assert.NotNull(viewResult.ViewData);
        }

        
    }
}
