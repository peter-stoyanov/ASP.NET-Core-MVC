using LanguageBuilder.Services.Contracts;
using LanguageBuilder.Web.Areas.Admin.Models;
using LanguageBuilder.Web.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using LanguageBuilder.Web.Infrastructure.Extensions;
using LanguageBuilder.Web.ViewComponents;
using LanguageBuilder.Data.Models;
using AutoMapper;
using LanguageBuilder.Services.Models;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.SignalR;
using LanguageBuilder.Web.Hubs;

namespace LanguageBuilder.Web.Areas.Admin.Controllers
{
    [Area(WebConstants.AdminArea)]
    public class UploadController : BaseController
    {
        private readonly IWordsService _wordsService;
        private readonly ILanguageService _languageService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMapper _mappper;
        private IHubContext<NotificationsHub> _hubContext;

        public UploadController(
            IWordsService wordsService,
            IUsersService usersService,
            ILanguageService languageService,
            IMapper mapper,
            IHubContext<NotificationsHub> hubContext,
            IHostingEnvironment hostingEnvironment)
            : base(usersService)
        {
            _wordsService = wordsService;
            _languageService = languageService;
            _hostingEnvironment = hostingEnvironment;
            _mappper = mapper;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> Words()
        {
            var model = new WordsUploadViewModel
            {
                Languages = (await _languageService.GetAllAsync()).ToList()
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Languages()
        {
            var model = _languageService
                .GetAllWithWordsCount()
                .Select(l => _mappper.Map<LanguageListingServiceModel, LanguageListingViewModel>(l))
                .ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult LanguageDelete()
        {
            

            return RedirectToAction(nameof(Languages));
        }

        [HttpPost]
        public async Task<IActionResult> Words(WordsUploadViewModel model)
        {
            try
            {
                model.Languages = (await _languageService.GetAllAsync()).ToList();

                if (!ModelState.IsValid)
                {
                    TempData.Put(WebConstants.AlertKey, new BootstrapAlertViewModel(BootstrapAlertType.Danger, "Please select a file.", hasDismissButton: true));

                    return RedirectToLocal("", nameof(UploadController.Words), "Upload");
                }

                if (model.File == null || model.File.Length == 0)
                {
                    TempData.Put(WebConstants.AlertKey, new BootstrapAlertViewModel(BootstrapAlertType.Danger, "Please select a file.", hasDismissButton: true));

                    return RedirectToLocal("", nameof(UploadController.Words), "Upload");
                }

                using (var memoryStream = new MemoryStream())
                {
                    await model.File.CopyToAsync(memoryStream).ConfigureAwait(false);

                    using (var package = new ExcelPackage(memoryStream))
                    {
                        var worksheet = package.Workbook.Worksheets[1];
                        var rowCount = worksheet.Dimension?.Rows;
                        var colCount = worksheet.Dimension?.Columns;

                        for (int row = 1; row <= rowCount.Value; row++)
                        {
                            var content = worksheet.Cells[row, 1].Value.ToString();
                            var definition = worksheet.Cells[row, 2].Value.ToString();
                            if (String.IsNullOrEmpty(content) || String.IsNullOrEmpty(definition))
                            {
                                continue;
                            }
                            if (await _wordsService.ExistAsync(content))
                            {
                                continue;
                            }

                            await _wordsService.AddAsync(new Word { Content = content, LanguageId = model.LanguageId, Definition = definition });
                        }
                    }
                }

                //TempData.Put(WebConstants.AlertKey, new BootstrapAlertViewModel(BootstrapAlertType.Success, "Word import has been successful.", hasDismissButton: true));

                return RedirectToAction(nameof(Languages));

            }
            catch (Exception ex)
            {
                ex.SaveToLog();

                TempData.Put(WebConstants.AlertKey, new BootstrapAlertViewModel(BootstrapAlertType.Danger, "Sorry but it seems that an error occured.", hasDismissButton: true));
            }

            return View(model);
        }
    }
}
