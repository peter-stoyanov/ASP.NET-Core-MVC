using Xunit;
using Xunit.Sdk;
using LanguageBuilder.Web.Controllers;
using Moq;
using LanguageBuilder.Services.Contracts;
using LanguageBuilder.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace LanguageBuilder.Tests.Web
{
    public class HomeControllerTests
    {
         [Fact]
        public void Index_ReturnsAViewResult_WithDefaultViewName()
        {
            var controller = new HomeController();

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Empty(viewResult.ViewName);
        }

        [Fact]
        public void Index_ReturnsAViewResult()
        {
            var mockUserService = new Mock<IUsersService>();

            var sut = new HomeController();

            var result = sut.Index() as ViewResult;

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Index_ReturnsAViewResult_WithViewDataWithoutModel()
        {
            var controller = new HomeController();

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            //Assert.Equal("Welcome to the Gatewood Elementary 'Bids For Kids' Auction Procurement Database!", viewResult.ViewData["Message"]);
            Assert.Null(viewResult.ViewData.Model);
        }
        
        [Fact]
        public void About_ReturnsView()
        {
            var mockUserService = new Mock<IUsersService>();

            var sut = new HomeController();

            var result = sut.About() as ViewResult;

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Contact_ReturnsView()
        {
            var mockUserService = new Mock<IUsersService>();

            var sut = new HomeController();

            var result = sut.Contact() as ViewResult;

            Assert.IsType<ViewResult>(result);
        }

        
    }
}
