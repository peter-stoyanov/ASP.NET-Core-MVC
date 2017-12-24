using AutoMapper;
using Hangfire;
using LanguageBuilder.Data;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Contracts;
using LanguageBuilder.Services.Implementations;
using LanguageBuilder.Services.Models;
using LanguageBuilder.Web.Areas.Admin.Models;
using LanguageBuilder.Web.BackgroundTasks;
using LanguageBuilder.Web.Controllers;
using LanguageBuilder.Web.Hubs;
using LanguageBuilder.Web.Infrastructure.Extensions;
using LanguageBuilder.Web.ViewComponents;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
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
        private readonly ILogger<BulkImportWordsBackgroundService> _logger;
        private readonly LanguageBuilderDbContext _context;
        private readonly IServiceProvider _serviceProvider;
        private IHubContext<NotificationsHub> _hubContext;

        public UploadController(
            IWordsService wordsService,
            IUsersService usersService,
            ILanguageService languageService,
            IMapper mapper,
            ILogger<BulkImportWordsBackgroundService> logger,
            IHubContext<NotificationsHub> hubContext,
            IServiceProvider serviceProvider,
            LanguageBuilderDbContext context,
            IHostingEnvironment hostingEnvironment)
            : base(usersService)
        {
            _wordsService = wordsService;
            _languageService = languageService;
            _hostingEnvironment = hostingEnvironment;
            _mappper = mapper;
            _logger = logger;
            _serviceProvider = serviceProvider;
            _context = context;
            _hubContext = hubContext;
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

                var words = await model.ParseExcelInputData();

                BackgroundJob.Enqueue(() => UploadWordsBackgroundService(words, model.LanguageId, HttpContext.User.Identity.Name));

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

                return RedirectToAction(nameof(Languages));
            }
            catch (Exception ex)
            {
                ex.SaveToLog();

                TempData.Put(WebConstants.AlertKey, new BootstrapAlertViewModel(BootstrapAlertType.Danger, WebConstants.GeneralError, hasDismissButton: true));
            }

            return View(model);
        }

        #region upload helpers

        [NonAction]
        public async Task UploadWordsBackgroundService(Dictionary<string, string> words, int languageId, string userIdentity)
        {
            if (!words.Any()) { return; }
            
            await _hubContext
                .Clients
                .Group(userIdentity)?
                .InvokeAsync("onBackgroundJobStarted", "Background job Started");

            string message = string.Empty;

            try
            {
                int counter = 0;

                foreach (var wordKvp in words)
                {
                    if (await _wordsService.ExistAsync(wordKvp.Key)) { continue; }

                    _wordsService.Add(new Word
                    {
                        Content = wordKvp.Key,
                        Definition = wordKvp.Value,
                        LanguageId = languageId
                    });

                    counter++;
                }

                message = $"Your words upload has finished. {counter} new words have been stored in the database.";
            }
            catch (Exception ex)
            {
                ex.SaveToLog();
                message = "Your words upload has not finished correctly. Please try again.";
            }

            await _hubContext
                .Clients
                .Group(userIdentity)?
                .InvokeAsync("onUploadWordsCompleted", message);
        }

        [NonAction]
        public async Task UploadTranslationsBackgroundService(Dictionary<string, string> translations, int sourceLanguageId, int targetLanguageId, string userIdentity)
        {
            if (!translations.Any()) { return; }

            await _hubContext
                .Clients
                .Group(userIdentity)?
                .InvokeAsync("onBackgroundJobStarted", "Background job Started");

            string message = string.Empty;

            try
            {
                int counter = 0;

                foreach (var wordKvp in translations)
                {
                    var source = wordKvp.Key;
                    var target = wordKvp.Key;

                    if (String.IsNullOrEmpty(source) || String.IsNullOrEmpty(target)) { continue; }

                    if (await _wordsService.ExistAsync(source)) { continue; }

                    if (await _wordsService.ExistAsync(target)) { continue; }

                    var sourceWord = new Word { Content = source, LanguageId = sourceLanguageId };
                    var targetWord = new Word { Content = target, LanguageId = targetLanguageId };

                    await _wordsService.AddWordsWithTranslationAsync(sourceWord, targetWord);

                    counter++;
                }

                message = $"Your translations upload has finished. {counter} new translations have been stored in the database.";
            }
            catch (Exception ex)
            {
                ex.SaveToLog();
                message = "Your translations upload has not finished correctly. Please try again.";
            }

            await _hubContext
                .Clients
                .Group(userIdentity)?
                .InvokeAsync("onUploadTranslationsCompleted", message);
        }

        #endregion
    }
}
