using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LanguageBuilder.Web.ViewComponents
{
    public class BootstrapAlertComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(BootstrapAlertViewModel model)
        {
            if (model != null && (model.StrongText == null || String.IsNullOrEmpty(model.StrongText)))
            {
                model.StrongText = $"{Enum.GetName(typeof(BootstrapAlertType), (int)model.Type)}.";
            }

            return View(model);
        }
    }
}
