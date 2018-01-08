using AutoMapper;
using Hangfire;
using LanguageBuilder.Data;
using LanguageBuilder.Services.Contracts;
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
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageBuilder.Web.Areas.Admin.Controllers
{
    [Area(WebConstants.ADMIN_AREA)]
    public class UploadController : BaseController
    {
        private readonly IWordsService _wordsService;
        private readonly ILanguageService _languageService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMapper _mappper;

        private readonly LanguageBuilderDbContext _context;

        private readonly IServiceProvider _serviceProvider;
        private IHubContext<NotificationsHub> _hubContext;

        public UploadController(
            IWordsService wordsService,
            IUsersService usersService,
            ILanguageService languageService,
            IMapper mapper,
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
                    TempData.Put(WebConstants.ALERTKEY, new BootstrapAlertViewModel(BootstrapAlertType.Danger, "Please select a file.", hasDismissButton: true));

                    return RedirectToLocal(string.Empty, nameof(UploadController.Words), "Upload");
                }

                if (model.File == null || model.File.Length == 0)
                {
                    TempData.Put(WebConstants.ALERTKEY, new BootstrapAlertViewModel(BootstrapAlertType.Danger, "Please select a file.", hasDismissButton: true));

                    return RedirectToLocal(string.Empty, nameof(UploadController.Words), "Upload");
                }

                var words = await model.ParseExcelInputData();

                BackgroundJob.Enqueue(() => new UploadWordsBackgroundJob(_wordsService, _hubContext).ExecuteAsync(words, model.LanguageId, HttpContext.User.Identity.Name));

                return RedirectToAction(nameof(Languages));
            }
            catch (Exception ex)
            {
                ex.SaveToLog();

                TempData.Put(WebConstants.ALERTKEY, new BootstrapAlertViewModel(BootstrapAlertType.Danger, WebConstants.GENERAL_ERROR, hasDismissButton: true));
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
                    TempData.Put(WebConstants.ALERTKEY, new BootstrapAlertViewModel(BootstrapAlertType.Danger, "Please select a file.", hasDismissButton: true));

                    return RedirectToLocal(string.Empty, nameof(UploadController.Translations), "Upload");
                }

                if (model.File == null || model.File.Length == 0)
                {
                    TempData.Put(WebConstants.ALERTKEY, new BootstrapAlertViewModel(BootstrapAlertType.Danger, "Please select a file.", hasDismissButton: true));

                    return RedirectToLocal(string.Empty, nameof(UploadController.Translations), "Upload");
                }

                var translations = await model.ParseExcelInputData();

                BackgroundJob.Enqueue(() => new UploadTranslationsBackgroundJob(_wordsService, _hubContext).ExecuteAsync(translations, model.FromLanguageId, model.ToLanguageId, HttpContext.User.Identity.Name));

                return RedirectToAction(nameof(Languages));
            }
            catch (Exception ex)
            {
                ex.SaveToLog();

                TempData.Put(WebConstants.ALERTKEY, new BootstrapAlertViewModel(BootstrapAlertType.Danger, WebConstants.GENERAL_ERROR, hasDismissButton: true));
            }

            return View(model);
        }
    }
}
