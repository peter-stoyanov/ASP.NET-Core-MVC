using LanguageBuilder.Services.Contracts;
using LanguageBuilder.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace LanguageBuilder.Tests.Web
{
    public class HomeControllerTests : BaseWebTest
    {
        private HomeController _homeController;

        public HomeControllerTests()
        {
            var mockUsersService = new Mock<IUsersService>();

            _homeController = new HomeController(mockUsersService.Object);
        }

        [Fact]
        public void Index_ReturnsAViewResult_WithDefaultViewName()
        {
            var result = _homeController.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
        }

        [Fact]
        public void Index_ReturnsAViewResult()
        {
            var mockUserService = new Mock<IUsersService>();

            var sut = _homeController;

            var result = sut.Index() as ViewResult;

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Index_ReturnsAViewResult_WithViewDataWithoutModel()
        {
            var result = _homeController.Index();

            var viewResult = Assert.IsType<ViewResult>(result);

            Assert.Null(viewResult.ViewData.Model);
        }

        [Fact]
        public void About_ReturnsView()
        {
            var mockUserService = new Mock<IUsersService>();

            var sut = _homeController;

            var result = sut.About() as ViewResult;

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Contact_ReturnsView()
        {
            var mockUserService = new Mock<IUsersService>();

            var sut = _homeController;

            var result = sut.Contact() as ViewResult;

            Assert.IsType<ViewResult>(result);
        }
    }
}
