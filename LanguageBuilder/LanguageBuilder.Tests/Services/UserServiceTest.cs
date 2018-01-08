using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Implementations;
using Xunit;

namespace LanguageBuilder.Tests.Services
{
    public class UserServiceTest : BaseServiceTest
    {
        [Fact]
        public void GetById_ReturnsUser_IfExisting()
        {
            // Arange
            var userService = new UsersService(InMemoryDatabase);

            string id = "id";

            var user = new User { Id = id };

            InMemoryDatabase.Users.Add(user);
            InMemoryDatabase.SaveChanges();

            // Act
            var result = userService.GetById(id);

            // Assert
            Assert.NotNull(result);
        }
    }
}
