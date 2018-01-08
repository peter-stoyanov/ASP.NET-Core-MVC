using AutoMapper;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Contracts;
using LanguageBuilder.Services.Models;
using LanguageBuilder.Web.Infrastructure.Extensions;
using LanguageBuilder.Web.ViewComponents;
using LanguageBuilder.Web.ViewModels.TranslationViewModels;
using LanguageBuilder.Web.ViewModels.WordViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageBuilder.Web.Controllers
{
    [Authorize]
    public class TranslationsController : BaseController
    {
        private readonly IWordsService _wordsService;
        private readonly ITranslationService _translationsService;
        private readonly ILanguageService _languageService;
        private readonly IMapper _mapper;

        public TranslationsController(
            IWordsService wordsService,
            ITranslationService translationsService,
            IUsersService usersService,
            ILanguageService languageService,
            IMapper mapper)
            : base(usersService)
        {
            _wordsService = wordsService;
            _translationsService = translationsService;
            _languageService = languageService;
            _mapper = mapper;
        }

        public async Task<IActionResult> My(TranslationsSearchFormViewModel searchForm, SortOptions sortOptions)
        {
            var request = searchForm.ToSearchRequest();

            request.Filter = t => t.TargetWord.Users.Any(u => u.UserId == LoggedUser.Id);

            var response = await _translationsService.Search(request, sortOptions);

            searchForm.Languages = (await _languageService.GetAllAsync()).ToList();

            var model = new TranslationsSearchViewModel
            {
                Response = response,
                SearchForm = searchForm
            };

            if (!model.Data.Any())
            {
                TempData.Put(WebConstants.ALERTKEY, new BootstrapAlertViewModel(BootstrapAlertType.Info, WebConstants.NORECORDS_MESSAGE, hasDismissButton: true));
            }

            return View(nameof(Search), model);
        }

        public async Task<IActionResult> Search(TranslationsSearchFormViewModel searchForm, SortOptions sortOptions)
        {
            var request = searchForm.ToSearchRequest();

            var response = await _translationsService.Search(request, sortOptions);

            searchForm.Languages = (await _languageService.GetAllAsync()).ToList();
            searchForm.SortOptions = sortOptions;

            var model = new TranslationsSearchViewModel
            {
                Response = response,
                SearchForm = searchForm
            };

            if (!model.Data.Any())
            {
                TempData.Put(WebConstants.ALERTKEY, new BootstrapAlertViewModel(BootstrapAlertType.Info, "There are no records in the database.", hasDismissButton: true));
            }

            return View(model);
        }

        // public async Task<IActionResult> Details(int? id)
        // {
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var word = await _wordsService.GetAsync(id.Value);

        //    if (word == null)
        //    {
        //        return NotFound();
        //    }

        //    var model = _mapper.Map<Word, WordDetailViewModel>(word);

        //    return View(model);
        // }

        public async Task<IActionResult> Create()
        {
            var model = new TranslationBaseViewModel
            {
                Languages = await _languageService.GetAllAsync()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TranslationCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _wordsService.AddWordsWithTranslationWithUserConnectionAsync(
                        _mapper.Map<WordCreateViewModel, Word>(model.SourceWord),
                        _mapper.Map<WordCreateViewModel, Word>(model.TargetWord),
                        this.LoggedUser.Id);

                    TempData.Put(WebConstants.ALERTKEY, new BootstrapAlertViewModel(BootstrapAlertType.Success, "Translation was succesfuly created.", hasDismissButton: true));

                    return RedirectToAction(nameof(My));
                }
            }
            catch (Exception ex)
            {
                ex.SaveToLog();

                TempData.Put(WebConstants.ALERTKEY, new BootstrapAlertViewModel(BootstrapAlertType.Danger, WebConstants.GENERAL_ERROR, hasDismissButton: true));
            }

            model.Languages = await _languageService.GetAllAsync();

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var translation = await _translationsService.GetWithLoadedWordsAsync(id);

            if (translation == null) { return NotFound(); }

            var model = _mapper.Map<Translation, TranslationEditViewModel>(translation);

            model.Languages = await _languageService.GetAllAsync();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TranslationEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _translationsService.DeleteAsync(model.Id);

                    await _wordsService.AddWordsWithTranslationWithUserConnectionAsync(
                        _mapper.Map<WordCreateViewModel, Word>(model.SourceWord),
                        _mapper.Map<WordCreateViewModel, Word>(model.TargetWord),
                        this.LoggedUser.Id);

                    TempData.Put(WebConstants.ALERTKEY, new BootstrapAlertViewModel(BootstrapAlertType.Success, WebConstants.UPDATED_GENERIC_MESSAGE, hasDismissButton: true));

                    return RedirectToAction(nameof(My));
                }
            }
            catch (Exception ex)
            {
                ex.SaveToLog();

                TempData.Put(WebConstants.ALERTKEY, new BootstrapAlertViewModel(BootstrapAlertType.Danger, WebConstants.GENERAL_ERROR, hasDismissButton: true));
            }

            model.Languages = await _languageService.GetAllAsync();

            return View(model);
        }

        public IActionResult Train()
        {
            return View();
        }

        public IActionResult Match()
        {
            return View();
        }

        public IActionResult Reproduce()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = WebConstants.ADMINISTRATOR_ROLE)]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var translation = await _translationsService.GetAsync(id);

                if (translation == null)
                {
                    return BadRequest("There was an errow while deleting the item. Please try again.");
                }

                await _translationsService.DeleteAsync(id);

                return Redirect(Request.UrlReferrer().ToString());
            }
            catch (Exception ex)
            {
                ex.SaveToLog();
            }

            return BadRequest("There was an errow while deleting the item. Please try again.");
        }
    }
}
