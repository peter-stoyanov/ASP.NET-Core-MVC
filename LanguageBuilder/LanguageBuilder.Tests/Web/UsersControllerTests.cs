using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Contracts;
using LanguageBuilder.Web.Areas.Admin.Controllers;
using LanguageBuilder.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using Xunit;

namespace LanguageBuilder.Tests.Web
{
    public class UsersControllerTests
    {
        [Fact]
        public async void Search_ReturnsView()
        {
            var mockUserService = new Mock<IUsersService>();
            var mockRolesService = new Mock<IRolesService>();

            var sut = new UsersController(mockUserService.Object, mockRolesService.Object);

            var searchForm = new UsersSearchFormViewModel();
            int? page = 1;
            var result = await sut.Search(searchForm, page) as ViewResult;

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ActionMethods_RequireAdministratorRole()
        {
            //Arrange
            //var methods = typeof(UsersController).GetMethods();
            var methods = GetMethodsOfReturnType(typeof(UsersController), typeof(ActionResult));

            //Act
            var attributes = new List<AuthorizeAttribute>();
            foreach (var method in methods)
            {
                var attribute = method.GetCustomAttribute(typeof(AuthorizeAttribute), true) as AuthorizeAttribute;

                attributes.Add(attribute);
            }

            //Assert
            Assert.All(attributes, attribute => Assert.True(attribute != null && attribute.Roles == LanguageBuilder.Web.WebConstants.ADMIN_AREA));
        }

        [Fact]
        public async Task EditRolesPost_ReturnsBadRequestResult_WhenModelStateIsInvalid()
        {
            // Arrange

            var mockUserService = new Mock<IUsersService>();
            var mockRolesService = new Mock<IRolesService>();

            var sut = new UsersController(mockUserService.Object, mockRolesService.Object);

            //sut.TempData = new TempDataDictionary();
            sut.ModelState.AddModelError("SessionName", "Required");

            var viewModel = new UserRolesViewModel();

            // Act
            var result = await sut.EditRoles(viewModel);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        #region test helpers

        public IEnumerable<MethodInfo> GetMethodsOfReturnType(Type cls, Type returnType)
        {
            // Did you really mean to prohibit public methods? I assume not
            var methods = cls.GetMethods(BindingFlags.NonPublic |
                                         BindingFlags.Public |
                                         BindingFlags.Instance);
            var retMethods = methods.Where(m => m.ReturnType.IsAssignableFrom(returnType));
            return retMethods;
        }

        #endregion
    }

}
