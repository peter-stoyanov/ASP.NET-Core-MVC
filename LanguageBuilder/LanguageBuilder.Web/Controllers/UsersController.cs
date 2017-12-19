using AutoMapper;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Contracts;
using LanguageBuilder.Web.Infrastructure.Extensions;
using LanguageBuilder.Web.ViewComponents;
using LanguageBuilder.Web.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LanguageBuilder.Web.Controllers
{
    [Authorize(Roles = WebConstants.UserRole)]
    public class UsersController : BaseController
    {
        private readonly IUsersService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUsersService userService, IMapper mapper) : base(userService)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Profile(string id)
        {
            var user = await _userService.GetByUsernameAsync(id);

            if (user == null) { return NotFound(); }

            var model = _mapper.Map<User, UserProfileViewModel>(user);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile(string id)
        {
            if (LoggedUser.Id != id) { return Unauthorized(); }

            var user = await _userService.GetByIdAsync(id);

            if (user == null) { return NotFound(); }

            var model = _mapper.Map<User, UserEditProfileViewModel>(user);

            //model.Subscriptions = _userService.

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(UserEditProfileViewModel model)
        {
            if (LoggedUser.Id != model.Id) { return Unauthorized(); }

            try
            {
                var user = await _userService.GetByIdAsync(model.Id);

                if (user == null) { return NotFound(); }

                _mapper.Map(model, user);

                await _userService.UpdateAsync(user);

                TempData.Put(WebConstants.AlertKey, new BootstrapAlertViewModel(BootstrapAlertType.Success, "User profile was successfully updated.", hasDismissButton: true));

                return RedirectToLocal(null, nameof(UsersController.Profile), "Users", new { area = "", id = model.Username });
            }
            catch (Exception ex)
            {
                ex.SaveToLog();
                TempData.Put(WebConstants.AlertKey, new BootstrapAlertViewModel(BootstrapAlertType.Danger, WebConstants.GeneralError, hasDismissButton: true));
            }

            return View(model);
        }
    }
}
