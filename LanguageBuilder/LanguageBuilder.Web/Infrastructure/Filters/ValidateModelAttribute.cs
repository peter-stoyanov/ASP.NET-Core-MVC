using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace LanguageBuilder.Web.Infrastructure.Filters
{
    public class ValidateModelStateAttributeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                var controller = actionContext.Controller as Controller;

                if (controller == null)
                {
                    return;
                }

                var model = actionContext
                    .ActionArguments
                    .FirstOrDefault(a => a.Key.ToLower().Contains("model"))
                    .Value;

                if (model == null)
                {
                    return;
                }

                actionContext.Result = controller.View(model);
            }
        }
    }
}
