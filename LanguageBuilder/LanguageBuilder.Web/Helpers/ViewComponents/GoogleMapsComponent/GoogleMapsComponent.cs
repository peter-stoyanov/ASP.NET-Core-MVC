using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LanguageBuilder.Web.ViewComponents
{
    public class GoogleMapsComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(GoogleMapsViewModel model)
        {
            return View(model);
        }
    }
}
