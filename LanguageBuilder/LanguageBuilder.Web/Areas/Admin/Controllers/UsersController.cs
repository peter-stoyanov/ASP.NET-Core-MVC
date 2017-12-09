using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Contracts;
using LanguageBuilder.Web.Areas.Admin.Models;
using LanguageBuilder.Web.Controllers;
using LanguageBuilder.Web.ViewComponents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static LanguageBuilder.Web.WebConstants;

namespace LanguageBuilder.Web.Areas.Admin.Controllers
{
    [Area(AdminArea)]
    [Authorize(Roles = AdministratorRole)]
    public class UsersController : BaseController
    {
        private readonly IUsersService _userService;
        private readonly UserManager<User> _userManager;

        public UsersController(
            IUsersService usersService,
            UserManager<User> userManager)
            : base(usersService)
        {
            this._userManager = userManager;
            this._userService = usersService;
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
                SearchForm = searchForm,
                Paging = new cloudscribe.Web.Pagination.PaginationSettings
                {
                    CurrentPage = response.Records.PageNumber,
                    TotalItems = 100, //response.Records.TotalItems,
                    ItemsPerPage = response.Records.PageSize,
                    MaxPagerItems = response.Records.TotalItems
                }
            };

            if (!model.Response.Records.Data.Any())
            {
                TempData[WebConstants.AlertKey] = new BootstrapAlertViewModel(BootstrapAlertType.Info, "There are no records in the database.", hasDismissButton: true);
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult User(WordsUploadViewModel model)
        {
           

            return View(model);
        }
    }
}
