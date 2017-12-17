using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageBuilder.Web.Extensions
{
    public static class ViewContextExtensions
    {
        public static string CurrentController(this ViewContext viewContext)
        {
            return viewContext.RouteData.Values["controller"]?.ToString().ToLower();
        }

        public static string CurrentAction(this ViewContext viewContext)
        {
            return viewContext.RouteData.Values["action"]?.ToString().ToLower();
        }

        public static string CurrentArea(this ViewContext viewContext)
        {
            return viewContext.RouteData.Values["area"]?.ToString().ToLower();
        }

        public static bool IsCurrentUrl(this ViewContext viewContext, string controller, string action)
        {
            return viewContext.CurrentController() == controller.ToLower()
                && viewContext.CurrentAction() == action.ToLower();
        }

        public static bool IsCurrentUrl(this ViewContext viewContext, string controller, IEnumerable<string> actions)
        {
            foreach (var action in actions)
            {
                if (viewContext.CurrentController() == controller.ToLower()
                    && viewContext.CurrentAction() == action.ToLower())
                {
                    return true;
                }
            }

            return false;
        }
    }
}
