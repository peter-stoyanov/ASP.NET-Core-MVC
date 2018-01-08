using LanguageBuilder.Services.Contracts;
using LanguageBuilder.Web.Areas.Admin.Models;
using LanguageBuilder.Web.Controllers;
using LanguageBuilder.Web.Infrastructure.Extensions;
using LanguageBuilder.Web.ViewComponents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using static LanguageBuilder.Web.WebConstants;

namespace LanguageBuilder.Web.Areas.Admin.Controllers
{
    [Area(ADMIN_AREA)]
    [Authorize(Roles = ADMINISTRATOR_ROLE)]
    public class UsersController : BaseController
    {
        private readonly IUsersService _userService;
        private readonly IRolesService _roleService;

        public UsersController(
            IUsersService usersService,
            IRolesService roleService)
            : base(usersService)
        {
            this._userService = usersService;
            this._roleService = roleService; ;
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

            if (model.Data != null && !model.Data.Any())
            {
                TempData.Put(WebConstants.ALERTKEY, new BootstrapAlertViewModel(BootstrapAlertType.Info, "There are no records in the database.", hasDismissButton: true));
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
                UserId = user.Id
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
                    TempData.Put(WebConstants.ALERTKEY, new BootstrapAlertViewModel(BootstrapAlertType.Danger, "Users can not update their own roles.", hasDismissButton: true));

                    return RedirectToLocal(model.Caller, nameof(UsersController.Search), "Users");
                }

                if (ModelState.IsValid)
                {
                    await _roleService.UpdateAsync(model.UserId, model.SelectedRoles);

                    TempData.Put(WebConstants.ALERTKEY, new BootstrapAlertViewModel(BootstrapAlertType.Success, "User roles were successfully updated.", hasDismissButton: true));

                    return RedirectToLocal(model.Caller, nameof(UsersController.Search), "Users");
                }
            }
            catch (Exception ex)
            {
                ex.SaveToLog();
            }

            model.Roles = (await _roleService.GetAllAsync()).ToList();
            model.SelectedRoles = (await _roleService.GetByUserIdAsync(model.UserId)).Select(r => r.Name).ToList();

            TempData.Put(WebConstants.ALERTKEY, new BootstrapAlertViewModel(BootstrapAlertType.Danger, WebConstants.GENERAL_ERROR, hasDismissButton: true));

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(string userId)
        {
            try
            {
                var user = await _userService.GetByIdAsync(userId);

                if (user == null)
                {
                    TempData.Put(WebConstants.ALERTKEY, new BootstrapAlertViewModel(BootstrapAlertType.Danger, "User not found.", hasDismissButton: true));

                    return RedirectToAction(nameof(Search));
                }

                await _userService.DeleteAsync(userId);

                TempData.Put(WebConstants.ALERTKEY, new BootstrapAlertViewModel(BootstrapAlertType.Success, "User was successfully deleted.", hasDismissButton: true));
            }
            catch (Exception ex)
            {
                ex.SaveToLog();

                TempData.Put(WebConstants.ALERTKEY, new BootstrapAlertViewModel(BootstrapAlertType.Danger, WebConstants.GENERAL_ERROR, hasDismissButton: true));
            }

            return RedirectToLocal(string.Empty, nameof(UsersController.Search), "Users");
        }
    }
}
