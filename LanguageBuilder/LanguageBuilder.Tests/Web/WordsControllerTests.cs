using AutoMapper;
using LanguageBuilder.Data;
using LanguageBuilder.Services.Contracts;
using LanguageBuilder.Tests.Web.Data;
using LanguageBuilder.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using Xunit;

namespace LanguageBuilder.Tests.Web
{
    public class WordsControllerTests : BaseWebTest
    {
        private IServiceProvider _serviceProvider;
        private Mock<ILanguageService> _mockLanguageService;
        private Mock<IWordsService> _mockWordsService;
        private Mock<IUsersService> _mockUsersService;
        private Mapper _mapper;

        public WordsControllerTests(Mapper mapper)
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

        [Fact]
        public void My_ReturnsOnlyLoggedUserTranslations()
        {
            //// Arrange
            //var controller = new WordsController(_mockWordsService.Object, _mockUsersService.Object, _mockLanguageService.Object, _mapper);
            //_mockWordsService
            //    .Setup(w => w.GetByUserAndLanguage(It.IsAny<User>(), It.IsAny<Language>())
            //    .Returns();

            //var searchForm = new WordsSearchFormViewModel();

            //// Act
            //var result = controller.My(searchForm);

            //// Assert
            //var viewResult = Assert.IsType<ViewResult>(result);
            //Assert.Null(viewResult.ViewName);
            //Assert.NotNull(viewResult.ViewData);
        }
    }
}
