using LanguageBuilder.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanguageBuilder.Web.Controllers
{
    public class ErrorsController : BaseController
    {
        public ErrorsController(IUsersService userService) : base(userService)
        { }

        [AllowAnonymous]
        public IActionResult Unknown() => View();

        [AllowAnonymous]
        public ActionResult NotFound() => View();

        [AllowAnonymous]
        public ActionResult NoAccess() => View();
    }
}
