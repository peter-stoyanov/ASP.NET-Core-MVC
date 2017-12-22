using AutoMapper;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Contracts;
using LanguageBuilder.Services.Models;
using LanguageBuilder.Web.Areas.Admin.Models;
using LanguageBuilder.Web.Controllers;
using LanguageBuilder.Web.Hubs;
using LanguageBuilder.Web.Infrastructure.Extensions;
using LanguageBuilder.Web.ViewComponents;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageBuilder.Web.Areas.Admin.Controllers
{
    [Area(WebConstants.AdminArea)]
    public class UploadController : BaseController
    {
        private readonly IWordsService _wordsService;
        private readonly ILanguageService _languageService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMapper _mappper;
        //private IHubContext<NotificationsHub> _hubContext;

        public UploadController(
            IWordsService wordsService,
            IUsersService usersService,
            ILanguageService languageService,
            IMapper mapper,
            //IHubContext<NotificationsHub> hubContext,
            IHostingEnvironment hostingEnvironment)
            : base(usersService)
        {
            _wordsService = wordsService;
            _languageService = languageService;
            _hostingEnvironment = hostingEnvironment;
            _mappper = mapper;
            //_hubContext = hubContext;
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
            // todo

            return RedirectToAction(nameof(Languages));
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

                            if (String.IsNullOrEmpty(content) || String.IsNullOrEmpty(definition)) { continue; }

                            if (await _wordsService.ExistAsync(content)) { continue; }

                            await _wordsService.AddAsync(new Word { Content = content, LanguageId = model.LanguageId, Definition = definition });
                        }
                    }
                }

                return RedirectToAction(nameof(Languages));
            }
            catch (Exception ex)
            {
                ex.SaveToLog();

                TempData.Put(WebConstants.AlertKey, new BootstrapAlertViewModel(BootstrapAlertType.Danger, WebConstants.GeneralError, hasDismissButton: true));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Translations()
        {
            var model = new TranslationsUploadViewModel
            {
                Languages = (await _languageService.GetAllAsync()).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Translations(TranslationsUploadViewModel model)
        {
            try
            {
                model.Languages = (await _languageService.GetAllAsync()).ToList();

                if (!ModelState.IsValid)
                {
                    TempData.Put(WebConstants.AlertKey, new BootstrapAlertViewModel(BootstrapAlertType.Danger, "Please select a file.", hasDismissButton: true));

                    return RedirectToLocal("", nameof(UploadController.Translations), "Upload");
                }

                if (model.File == null || model.File.Length == 0)
                {
                    TempData.Put(WebConstants.AlertKey, new BootstrapAlertViewModel(BootstrapAlertType.Danger, "Please select a file.", hasDismissButton: true));

                    return RedirectToLocal("", nameof(UploadController.Translations), "Upload");
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
                            var source = worksheet.Cells[row, 1].Value.ToString();
                            var target = worksheet.Cells[row, 2].Value.ToString();

                            if (String.IsNullOrEmpty(source) || String.IsNullOrEmpty(target)) { continue; }

                            if (await _wordsService.ExistAsync(source)) { continue; }

                            if (await _wordsService.ExistAsync(target)) { continue; }

                            var sourceWord = new Word { Content = source, LanguageId = model.FromLanguageId };
                            var targetWord = new Word { Content = target, LanguageId = model.ToLanguageId };

                            await _wordsService.AddWordsWithTranslationAsync(sourceWord, targetWord);
                        }
                    }
                }

                return RedirectToAction(nameof(Languages));
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
