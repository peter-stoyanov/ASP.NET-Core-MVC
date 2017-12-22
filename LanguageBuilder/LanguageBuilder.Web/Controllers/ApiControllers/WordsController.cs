using AutoMapper;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Contracts;
using LanguageBuilder.Web.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageBuilder.Web.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    public class WordsController : BaseController
    {
        private readonly IWordsService _wordsService;
        private readonly ITranslationService _translationService;
        private readonly IMapper _mapper;

        public WordsController(
            IWordsService wordsService,
            ITranslationService translationService,
            IUsersService userService,
            IMapper mapper)
            : base(userService)
        {
            _wordsService = wordsService;
            _translationService = translationService;
            _mapper = mapper;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]string keywords, [FromQuery]int languageId, [FromQuery]int rows = 10)
        {
            var words = await _wordsService.SearchAsync(keywords, languageId, rows);

            var response = new List<WordDTO>();

            foreach (var word in words)
            {
                response.Add(_mapper.Map<Word, WordDTO>(word));
            }

            return Ok(response);
        }

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
