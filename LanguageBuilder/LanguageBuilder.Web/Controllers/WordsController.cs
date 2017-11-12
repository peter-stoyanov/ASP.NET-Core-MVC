using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LanguageBuilder.Data;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Contracts;
using LanguageBuilder.Web.Models.WordViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace LanguageBuilder.Web.Controllers
{
    [Authorize]
    public class WordsController : BaseController
    {
        private readonly IWordsService _wordsService;
        private readonly IMapper _mapper;

        public WordsController(IWordsService wordsService, IUsersService usersService, IMapper mapper)
            : base(usersService)
        {
            _wordsService = wordsService;
            _mapper = mapper;
        }

        // GET: Words
        public async Task<IActionResult> Index()
        {
            var words = await _wordsService.GetByUserIdAsync(this.LoggedUser.Id);

            var model = new SearchViewModel
            {
                Words = words
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

            var word = await _wordsService.FindByIdAsync(id.Value);

            if (word == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<Word, DetailViewModel>(word);

            return View(model);
        }

        //// GET: Words/Create
        //public IActionResult Create()
        //{
        //    ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name");
        //    return View();
        //}

        //// POST: Words/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Gender,SyntaxType,Content,LanguageId,IsDeleted")] Word word)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(word);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name", word.LanguageId);
        //    return View(word);
        //}

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
            if (!await _wordsService.ExistInUserAsync(id, this.LoggedUser.Id))
            {
                TempData["ErrorMsg"] = "This word is no more present in the database.";
            }

            await _wordsService.SoftDeleteInUserAsync(id, this.LoggedUser.Id);

            TempData["SuccessMsg"] = "Word is successfully deleted.";

            return RedirectToAction(nameof(Index));
        }
    }
}
