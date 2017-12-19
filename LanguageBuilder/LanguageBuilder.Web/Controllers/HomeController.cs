using LanguageBuilder.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using LanguageBuilder.Services.Contracts;
using System.Web.Http;
using LanguageBuilder.Data.Models;
using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LanguageBuilder.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseAnonymousController
    {
        public HomeController(IUsersService usersService) : base (usersService)
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
