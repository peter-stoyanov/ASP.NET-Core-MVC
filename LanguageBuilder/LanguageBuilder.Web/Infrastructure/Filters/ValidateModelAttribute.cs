using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace LanguageBuilder.Web.Infrastructure.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false)
            {
                actionContext.Result = new BadRequestObjectResult(
                    actionContext.ModelState.Values
                        .SelectMany(e => e.Errors)
                        .Select(e => e.ErrorMessage));
            }

            //if (!actionContext.ModelState.IsValid)
            //{
            //    var controller = actionContext.Controller as Controller;

            //    if (controller == null)
            //    {
            //        return;
            //    }

            //    var model = actionContext
            //        .ActionArguments
            //        .FirstOrDefault(a => a.Key.ToLower().Contains("model"))
            //        .Value;

            //    if (model == null)
            //    {
            //        return;
            //    }

            //    actionContext.Result = controller.View(model);
            //}
        }
    }
}
