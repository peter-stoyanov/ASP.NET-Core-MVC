using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ComponentLibrary
{
    public class BootstrapAlertComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(BootstrapAlertViewModel model)
        {
            return View(model);
        }
    }
}
