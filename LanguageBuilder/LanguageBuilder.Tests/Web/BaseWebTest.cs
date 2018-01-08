using LanguageBuilder.Data;
using LanguageBuilder.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Security.Claims;

namespace LanguageBuilder.Tests.Web
{
    public class BaseWebTest : BaseTest
    {
        protected BaseWebTest()
        {
            TestStartup.Initialize();
        }

        protected LanguageBuilderDbContext Database
        {
            get
            {
                DbContextOptions<LanguageBuilderDbContext> options = new DbContextOptionsBuilder<LanguageBuilderDbContext>()
                   .UseInMemoryDatabase(Guid.NewGuid().ToString())
                   .Options;

                return new LanguageBuilderDbContext(options);
            }
        }

        protected void SetHttpContextWithUser(BaseController controller, string userNameClaim)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                 new Claim(ClaimTypes.Name, userNameClaim)
            }));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }

        protected T DeserializeFromJson<T>(object json)
            where T : class
        {
            return json == null ? null : JsonConvert.DeserializeObject<T>((string)json);
        }
    }
}
