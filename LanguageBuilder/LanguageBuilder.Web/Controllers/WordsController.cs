using AutoMapper;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Contracts;
using LanguageBuilder.Web.Infrastructure.Extensions;
using LanguageBuilder.Web.ViewModels.TranslationViewModels;
using LanguageBuilder.Web.ViewModels.WordViewModels;
using LanguageBuilder.Web.ViewComponents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using LanguageBuilder.Web.Extensions;

namespace LanguageBuilder.Web.Controllers
{
    [Authorize]
    public class WordsController : BaseController
    {
        private readonly IWordsService _wordsService;
        private readonly ILanguageService _languageService;
        private readonly IMapper _mapper;

        public WordsController(
            IWordsService wordsService,
            IUsersService usersService,
            ILanguageService languageService,
            IMapper mapper)
            : base(usersService)
        {
            _wordsService = wordsService;
            _languageService = languageService;
            _mapper = mapper;
        }

        // GET: Words
        public async Task<IActionResult> Index()
        {
            var words = await _wordsService.GetByUserIdAsync(this.LoggedUser.Id);

            var model = new WordsSearchViewModel
            {
                //Words = words
            };

            TempData.Put(WebConstants.AlertKey, new BootstrapAlertViewModel(BootstrapAlertType.Danger, "Test", "Strong", false));

            return View(model);
        }

        // GET: Words
        public async Task<IActionResult> Search(WordsSearchFormViewModel searchForm)
        {
            var request = searchForm.ToSearchRequest();

            var response = await _wordsService.Search(
                request, 
                sortColumnSelector: w => w.Content,
                criteria: w => w.Content.StartsWith(searchForm.SelectedLetter.ToLower()));

            searchForm.Languages = (await _languageService.GetAllAsync()).ToList();

            var model = new WordsSearchViewModel
            {
                Response = response,
                SearchForm = searchForm
                
            };

            return View(model);
        }

        // GET: Words/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var word = await _wordsService.GetAsync(id.Value);

            if (word == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<Word, WordDetailViewModel>(word);

            return View(model);
        }

        // GET: Words/Create
        public async Task<IActionResult> Create()
        {
            var model = new TranslationBaseViewModel();

            model.Languages = await _languageService.GetAllAsync();

            return View(model);
        }

        // POST: Words/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TranslationCreateViewModel model)
        {
            //try-catch ?

            if (ModelState.IsValid)
            {
                await _wordsService.AddWordsWithTranslationAsync(
                    _mapper.Map<WordCreateViewModel, Word>(model.SourceWord),
                    _mapper.Map<WordCreateViewModel, Word>(model.TargetWord),
                    this.LoggedUser.Id);

                return RedirectToAction(nameof(Index));
            }

            model.Languages = await _languageService.GetAllAsync();
            return View(model);
        }

        //// GET: Words/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var word = await _context.Words.SingleOrDefaultAsync(m => m.Id == id);
        //    if (word == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name", word.LanguageId);
        //    return View(word);
        //}

        //// POST: Words/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Gender,SyntaxType,Content,LanguageId,IsDeleted")] Word word)
        //{
        //    if (id != word.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(word);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!WordExists(word.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name", word.LanguageId);
        //    return View(word);
        //}

        //// GET: Words/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var word = await _context.Words
        //        .Include(w => w.Language)
        //        .SingleOrDefaultAsync(m => m.Id == id);
        //    if (word == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(word);
        //}

        // POST: Words/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if ((await _wordsService.ExistInUserAsync(id, this.LoggedUser.Id)) == false)
            {
                TempData["ErrorMsg"] = "This word is no more present in the database.";
            }

            await _wordsService.SoftDeleteInUserAsync(id, this.LoggedUser.Id);

            TempData["SuccessMsg"] = "Word is successfully deleted.";

            return RedirectToLocal(this.Request.BaseUrl());
        }
    }
}
