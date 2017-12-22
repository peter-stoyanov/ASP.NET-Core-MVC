using LanguageBuilder.Data;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LanguageBuilder.Tests.Services
{
    public class UserServiceTest
    {
        private readonly LanguageBuilderDbContext _context;

        public UserServiceTest()
        {
            _context = Tests.GetDb();
        }

        [Fact]
        public void ReturnsExistingUser()
        {
            // Arange
            var userService = new UsersService(_context);

            string guid = new Guid().ToString();

            var user = new User { Id = guid };

            _context.Users.Add(user);
            _context.SaveChanges();

            // Act
            var result = userService.GetById(guid);

            // Assert
            Assert.NotNull(result);
        }
    }
}
