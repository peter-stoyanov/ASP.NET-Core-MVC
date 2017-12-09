using LanguageBuilder.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageBuilder.Web.Controllers
{
    public class ErrorsController : BaseController
    {
        public ErrorsController(IUsersService userService) : base(userService)
        { }

        [AllowAnonymous]
        public IActionResult Unknown()
        {
            try
            {
                //Exception ex = IServer.GetLastError();

                //if (ex != null)
                //{
                //    ex.SaveToLog();
                //}
            }
            catch { }

            return View();
        }

        [AllowAnonymous]
        public ActionResult NotFound()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult NoAccess()
        {
            return View();
        }
    }
}
