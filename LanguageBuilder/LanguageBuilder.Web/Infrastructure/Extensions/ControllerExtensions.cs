using Microsoft.AspNetCore.Mvc;

namespace LanguageBuilder.Web.Infrastructure.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult ViewOrNotFound(this Controller controller, object model)
        {
            if (model == null)
            {
                return controller.NotFound();
            }

            return controller.View(model);
        }

        public static string ShortName(this Controller controller)
        {
            var name = controller.GetType().Name;

            return name.EndsWith("Controller") ? name.Substring(0, name.Length - "Controller".Length) : name;
        }
    }
}
