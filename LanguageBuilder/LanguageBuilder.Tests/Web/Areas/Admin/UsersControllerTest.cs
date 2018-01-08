using FluentAssertions;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Contracts;
using LanguageBuilder.Services.Models.UsersSearch;
using LanguageBuilder.Web;
using LanguageBuilder.Web.Areas.Admin.Controllers;
using LanguageBuilder.Web.Areas.Admin.Models;
using LanguageBuilder.Web.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace LanguageBuilder.Tests.Web.Areas.Admin
{
    public class UsersControllerTest : BaseWebTest
    {
        [Fact]
        public async void Search_ReturnsView()
        {
            // Arrange
            var response = new Response();
            var mockUserService = new Mock<IUsersService>();
            mockUserService
                .Setup(u => u.Search(It.IsAny<Request>(), It.IsAny<Expression<Func<User, object>>>()))
                .Returns(Task.FromResult<Response>(response));

            var mockRolesService = new Mock<IRolesService>();

            var sut = new UsersController(mockUserService.Object, mockRolesService.Object);

            var searchForm = new UsersSearchFormViewModel();
            searchForm.Keywords = "a";
            int? page = 1;

            // Act
            var result = await sut.Search(searchForm, page) as ViewResult;

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Controller_ShouldRequireAdministratorRole()
        {
            // Arrange
            var controller = typeof(UsersController);

            // Act
            var attribute = controller
                .GetCustomAttributes(true)
                .FirstOrDefault(a => a.GetType() == typeof(AreaAttribute)) as AreaAttribute;

            // Assert
            attribute.Should().NotBeNull();
            attribute.RouteValue.Should().Be(WebConstants.ADMIN_AREA);
        }

        [Fact]
        public async Task EditRolesPost_ReturnsBadRequestResult_WhenModelStateIsInvalid()
        {
            // Arrange
            BootstrapAlertViewModel alert = null;
            var mockUserService = new Mock<IUsersService>();

            var mockRolesService = new Mock<IRolesService>();

            var tempData = new Mock<ITempDataDictionary>();
            tempData
                .SetupSet(t => t[WebConstants.ALERTKEY] = It.IsAny<string>())
                .Callback((string key, object alertViewModel) =>
                {
                    alert = DeserializeFromJson<BootstrapAlertViewModel>(alertViewModel);
                });

            var sut = new UsersController(mockUserService.Object, mockRolesService.Object)
            {
                TempData = tempData.Object
            };

            sut.ModelState.AddModelError(String.Empty, "General Error");

            SetHttpContextWithUser(sut, "Username");

            var viewModel = new UserRolesViewModel();

            // Act
            var result = await sut.EditRoles(viewModel);

            // Assert
            alert.Should().NotBeNull();
            alert.Type.Should().Be(BootstrapAlertType.Danger);
            alert.Text.Should().Be(WebConstants.GENERAL_ERROR);

            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public async Task EditRolesPost_RedirectsToUsersSearch_WhenUserTriesToEditTheirOwnRoles()
        {
            // Arrange
            string userId = "id";
            BootstrapAlertViewModel alert = null;
            var mockUserService = new Mock<IUsersService>();
            mockUserService
                .Setup(u => u.GetByIdentity(It.IsAny<string>()))
                .Returns(new User { Id = userId });

            var mockRolesService = new Mock<IRolesService>();

            var tempData = new Mock<ITempDataDictionary>();
            tempData
                .SetupSet(t => t[WebConstants.ALERTKEY] = It.IsAny<string>())
                .Callback((string key, object alertViewModel) =>
                {
                    alert = DeserializeFromJson<BootstrapAlertViewModel>(alertViewModel);
                });

            var sut = new UsersController(mockUserService.Object, mockRolesService.Object)
            {
                TempData = tempData.Object
            };

            SetHttpContextWithUser(sut, "Username");

            var viewModel = new UserRolesViewModel { UserId = userId };

            // Act
            var result = await sut.EditRoles(viewModel);

            // Assert
            alert.Should().NotBeNull();
            alert.Type.Should().Be(BootstrapAlertType.Danger);
            alert.Text.Should().Be("Users can not update their own roles.");

            result.Should().BeOfType<RedirectToActionResult>();
            result.As<RedirectToActionResult>().ActionName.Should().Be(nameof(UsersController.Search));
            result.As<RedirectToActionResult>().ControllerName.Should().Be("Users");
        }

        [Fact]
        public async Task EditRolesPost_UpdatesUserRoles_WhenModelStateIsValid()
        {
            // Arrange
            BootstrapAlertViewModel alert = null;
            var mockUserService = new Mock<IUsersService>();
            mockUserService
                .Setup(u => u.GetByIdentity(It.IsAny<string>()))
                .Returns(new User { Id = "Id" });

            var mockRolesService = new Mock<IRolesService>();
            mockRolesService
                .Setup(r => r.UpdateAsync(It.IsAny<string>(), It.IsAny<IEnumerable<string>>()))
                .Returns(Task.CompletedTask);

            var tempData = new Mock<ITempDataDictionary>();
            tempData
                .SetupSet(t => t[WebConstants.ALERTKEY] = It.IsAny<string>())
                .Callback((string key, object alertViewModel) =>
                {
                    alert = DeserializeFromJson<BootstrapAlertViewModel>(alertViewModel);
                });

            var sut = new UsersController(mockUserService.Object, mockRolesService.Object)
            {
                TempData = tempData.Object
            };

            sut.ModelState.AddModelError(string.Empty, "General Error");

            SetHttpContextWithUser(sut, "Username");

            var viewModel = new UserRolesViewModel();

            // Act
            var result = await sut.EditRoles(viewModel);

            // Assert
            alert.Should().NotBeNull();
            alert.Type.Should().Be(BootstrapAlertType.Success);
            alert.Text.Should().Be("User roles were successfully updated.");

            mockRolesService
                .Verify(r => r.UpdateAsync(It.IsAny<string>(), It.IsAny<IEnumerable<string>>()), Times.Once);

            result.Should().BeOfType<RedirectToActionResult>();
            result.As<RedirectToActionResult>().ActionName.Should().Be(nameof(UsersController.Search));
            result.As<RedirectToActionResult>().ControllerName.Should().Be("Users");
        }
    }
}
