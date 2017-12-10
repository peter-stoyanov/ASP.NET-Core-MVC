using LanguageBuilder.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using LanguageBuilder.Services.Contracts;
using LanguageBuilder.Web.ViewModels.UserViewModels;
using System.Threading.Tasks;
using AutoMapper;
using LanguageBuilder.Data.Models;

namespace LanguageBuilder.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //return RedirectToAction(nameof(HomeController.About), "Home");

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
