using AutoMapper;
using LanguageBuilder.Data;
using LanguageBuilder.Services.Contracts;
using LanguageBuilder.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using Xunit;

namespace LanguageBuilder.Tests.Web
{
    public class WordsControllerTest : BaseWebTest
    {
        private IServiceProvider _serviceProvider;
        private Mock<ILanguageService> _mockLanguageService;
        private Mock<IWordsService> _mockWordsService;
        private Mock<IUsersService> _mockUsersService;
        private Mapper _mapper;

        public WordsControllerTest(Mapper mapper)
        {
            _mockWordsService = new Mock<IWordsService>();
            _mockUsersService = new Mock<IUsersService>();
            _mockLanguageService = new Mock<ILanguageService>();
            _mapper = mapper;
        }

        [Fact]
        public void Index_ReturnsView()
        {
            // Arrange
            var dbContext = _serviceProvider.GetRequiredService<LanguageBuilderDbContext>();
            var mockUsersService = new Mock<IUsersService>();
            var controller = new HomeController(mockUsersService.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
            Assert.NotNull(viewResult.ViewData);
        }
    }
}
