using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageBuilder.Web.Infrastructure.Extensions;

namespace LanguageBuilder.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly IUsersService _usersService;
        private User _loggedUser = null;

        public BaseController(IUsersService userService)
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
            //ViewBag.BuildVersion = this.GetType().Assembly.GetName().Version.ToString();
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

        
        //protected ActionResult RedirectToHttpNotFound()
        //{
        //    return this.RedirectToAction("NotFound", "Errors", new { path = this.Request.Path });
        //}

        //protected ActionResult RedirectToHttpForbidden()
        //{
        //    return this.RedirectToAction("NoAccess", "Errors", new { path = this.Request.Path });
        //}

        //public string RootUrl
        //{
        //    get
        //    {
        //        return string.Format("{0}://{1}", Request.Url.Scheme, Request.Url.Authority);
        //    }
        //}
    }
}
