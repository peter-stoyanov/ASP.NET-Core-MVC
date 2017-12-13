using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Contracts;
using LanguageBuilder.Web.Areas.Admin.Models;
using LanguageBuilder.Web.Controllers;
using LanguageBuilder.Web.Extensions;
using LanguageBuilder.Web.Infrastructure.Extensions;
using LanguageBuilder.Web.ViewComponents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using static LanguageBuilder.Web.WebConstants;

namespace LanguageBuilder.Web.Areas.Admin.Controllers
{
    [Area(AdminArea)]
    [Authorize(Roles = AdministratorRole)]
    public class UsersController : BaseController
    {
        private readonly IUsersService _userService;
        private readonly RoleManager<Role> _roleManager;
        private readonly IRolesService _roleService;
        private readonly UserManager<User> _userManager;

        public UsersController(
            IUsersService usersService,
            RoleManager<Role> roleManager,
            IRolesService roleService,
            UserManager<User> userManager)
            : base(usersService)
        {
            this._userManager = userManager;
            this._userService = usersService;
            this._roleService = roleService; ;
            this._roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Search(UsersSearchFormViewModel searchForm, int? page)
        {
            var request = searchForm.ToSearchRequest();

            if (page.HasValue) { request.PageNumber = page.Value; }

            var response = await _userService.Search(
                request,
                sortColumnSelector: u => u.UserName);

            var model = new UsersSearchViewModel
            {
                Response = response,
                SearchForm = searchForm
            };

            if (!model.Data.Any())
            {
                TempData.Put(WebConstants.AlertKey, new BootstrapAlertViewModel(BootstrapAlertType.Info, "There are no records in the database.", hasDismissButton: true));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditRoles(string id)
        {
            if (String.IsNullOrEmpty(id)) { return BadRequest(); }

            var user = await _userService.GetByIdAsync(id);

            if (user == null) { return NotFound(); }

            var model = new UserRolesViewModel
            {
                Roles = (await _roleService.GetAllAsync()).ToList(),
                SelectedRoles = (await _roleService.GetByUserIdAsync(id)).Select(r => r.Name).ToList(),
                User = user,
                UserId = user.Id,
                //Caller = Request.UrlReferrer()?.AbsoluteUri
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRoles(UserRolesViewModel model)
        {
            try
            {
                if (model.UserId == LoggedUser.Id)
                {
                    TempData.Put(WebConstants.AlertKey, new BootstrapAlertViewModel(BootstrapAlertType.Success, "Users can not update their own roles.", hasDismissButton: true));

                    return RedirectToLocal(model.Caller, nameof(UsersController.Search), "Users");
                }

                await _roleService.UpdateAsync(model.UserId, model.SelectedRoles);

                TempData.Put(WebConstants.AlertKey, new BootstrapAlertViewModel(BootstrapAlertType.Success, "User roles were successfully updated.", hasDismissButton: true));

                return RedirectToLocal(model.Caller, nameof(UsersController.Search), "Users");
            }
            catch (Exception ex)
            {
                ex.SaveToLog();
            }

            model.Roles = (await _roleService.GetAllAsync()).ToList();
            model.SelectedRoles = (await _roleService.GetByUserIdAsync(model.UserId)).Select(r => r.Name).ToList();

            TempData.Put(WebConstants.AlertKey, new BootstrapAlertViewModel(BootstrapAlertType.Danger, "We are sorry, but it seems that an error occured.", hasDismissButton: true));

            return View(model);
        }
    }
}
