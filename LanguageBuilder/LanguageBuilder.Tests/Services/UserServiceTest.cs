using LanguageBuilder.Data;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LanguageBuilder.Tests.Services
{
    public class UserServiceTest : BaseServiceTest
    {
        public UserServiceTest()
        {

        }

        [Fact]
        public void GetById_ReturnsUser_IfExisting()
        {
            // Arange
            var userService = new UsersService(InMemoryDatabase);

            string guid = new Guid().ToString();

            var user = new User { Id = guid };

            InMemoryDatabase.Users.Add(user);
            InMemoryDatabase.SaveChanges();

            // Act
            var result = userService.GetById(guid);

            // Assert
            Assert.NotNull(result);
        }
    }
}
