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
    public class TranslationsController : BaseController
    {
        private readonly IWordsService _wordsService;
        private readonly ITranslationService _translationService;
        private readonly IMapper _mapper;

        public TranslationsController(
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
        public async Task<IActionResult> Get([FromQuery]string userId, [FromQuery]string languageId)
        {
            if (LoggedUser.Id != userId)
            {
                return Unauthorized();
            }

            if (String.IsNullOrEmpty(userId) || String.IsNullOrEmpty(languageId))
            {
                return BadRequest();
            }

            var translations = await _translationService.GetByUserAndLanguageAsync(userId, languageId);

            var response = new List<TranslationDTO>();

            foreach (var translation in translations)
            {
                response.Add(_mapper.Map<Translation, TranslationDTO>(translation));
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
