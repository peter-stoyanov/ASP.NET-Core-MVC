using LanguageBuilder.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LanguageBuilder.Web.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    public class WordsController : BaseController
    {
        private readonly IWordsService _wordsService;
        private readonly ITranslationService _translationService;

        public WordsController(IWordsService wordsService, ITranslationService translationService, IUsersService userService)
            : base(userService)
        {
            _wordsService = wordsService;
            _translationService = translationService;
        }

        //[HttpGet]
        //public async Task<IEnumerable<object>> Search([FromBody]string keywords, [FromBody]int rows)
        //{
        //    return await _wordsService.SearchAsync(keywords, rows);
        //}

        //[HttpGet]
        //public async Task<IEnumerable<object>> GetByUser(string userId)
        //{
        //    return await _wordsService.GetByUserIdAsync(userId);
        //}

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

            return new ObjectResult(await _translationService.GetByUserAndLanguageAsync(userId, languageId));
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
