using Microsoft.AspNetCore.Mvc;

namespace LanguageBuilder.Web.Controllers
{
    public class RssController : Controller
    {
        public IActionResult Feed(string type)
        {
            //var posts = db.Posts.ToList();

            return View();
        }
    }
}
