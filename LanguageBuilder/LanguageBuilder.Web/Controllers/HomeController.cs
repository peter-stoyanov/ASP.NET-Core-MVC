using LanguageBuilder.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using LanguageBuilder.Services.Contracts;

namespace LanguageBuilder.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IUsersService userService) : base(userService)
        { }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

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
