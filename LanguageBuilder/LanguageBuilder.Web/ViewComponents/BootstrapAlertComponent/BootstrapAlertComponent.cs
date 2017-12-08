using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LanguageBuilder.Web.ViewComponents
{
    public class BootstrapAlertComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(BootstrapAlertViewModel model)
        {
            return View(model);
        }
    }
}
