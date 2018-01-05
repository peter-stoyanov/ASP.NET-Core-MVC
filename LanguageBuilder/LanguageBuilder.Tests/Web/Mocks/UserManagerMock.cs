using LanguageBuilder.Data.Models;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageBuilder.Tests.Web.Mocks
{
    public class UserManagerMock
    {
        public static Mock<UserManager<User>> New
        {
            get
            {
                return new Mock<UserManager<User>>(
                    Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            }
        }
    }
}
