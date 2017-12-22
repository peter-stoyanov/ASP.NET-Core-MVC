using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Contracts;
using LanguageBuilder.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace LanguageBuilder.Web.Controllers
{
    public class BaseAnonymousController : Controller
    {
        private readonly IUsersService _usersService;
        private User _loggedUser = null;

        public BaseAnonymousController(IUsersService userService)
        {
            _usersService = userService;
        }

        public User LoggedUser
        {
            get
            {
                if (this._loggedUser == null)
                {
                    if (this.User != null && this.User.Identity != null && !String.IsNullOrEmpty(this.User.Identity.Name))
                    {
                        this._loggedUser = _usersService.GetByIdentity(this.User.Identity.Name);
                    }
                }

                return this._loggedUser;
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            ViewBag.LoggedUser = this.LoggedUser;
        }

        protected IActionResult RedirectToReferrer(string defaultActionName = null, string defaultControllerName = null, object defaultRouteValues = null)
        {
            string url = this.Request.UrlReferrer() != null ? this.Request.UrlReferrer().PathAndQuery : null;

            return this.RedirectToLocal(url, defaultActionName, defaultControllerName: defaultControllerName, defaultRouteValues: defaultRouteValues);
        }

        protected IActionResult RedirectToLocal(string url, string defaultActionName = null, string defaultControllerName = null, object defaultRouteValues = null)
        {
            var baseUrl = this.Request.BaseUrl(addTrailingSlash: false);

            url = url != null && url.StartsWith(baseUrl) ? url.Substring(baseUrl.Length) : url;

            if (!String.IsNullOrWhiteSpace(url) && Url.IsLocalUrl(url))
            {
                return Redirect(url);
            }
            else
            {
                return RedirectToAction(defaultActionName, defaultControllerName, defaultRouteValues);
            }
        }

        protected ActionResult RedirectToHttpNotFound()
        {
            return this.RedirectToAction("NotFound", "Errors", new { path = this.Request.Path });
        }

        protected ActionResult RedirectToHttpForbidden()
        {
            return this.RedirectToAction("NoAccess", "Errors", new { path = this.Request.Path });
        }
    }
}
