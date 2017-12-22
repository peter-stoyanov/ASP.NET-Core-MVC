using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Contracts;
using LanguageBuilder.Web.Areas.Admin.Controllers;
using LanguageBuilder.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace LanguageBuilder.Tests.Web
{
    public class AdminControllerTest
    {
        [Fact]
        public async void Search_ReturnsView()
        {
            var mockUserService = new Mock<IUsersService>();
            var mockRoleManager = new Mock<RoleManager<Role>>();
            var mockRolesService = new Mock<IRolesService>();
            var mockUserManager = new Mock<UserManager<User>>();

            var sut = new UsersController(mockUserService.Object, mockRoleManager.Object, mockRolesService.Object, mockUserManager.Object);

            var searchForm = new UsersSearchFormViewModel();
            int? page = 1;
            var result = await sut.Search(searchForm, page) as ViewResult;

            Assert.IsType<ViewResult>(result);
        }
    }
}
