using LanguageBuilder.Services.Contracts;
using LanguageBuilder.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Web.Http;
using System;

namespace LanguageBuilder.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseAnonymousController
    {
        public HomeController(IUsersService usersService) : base(usersService)
        { }

        [ResponseCache(Duration = 60)]
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 60)]
        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 60)]
        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
