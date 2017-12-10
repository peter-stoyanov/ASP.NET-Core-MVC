using LanguageBuilder.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageBuilder.API.Controllers
{
    [Route("api/[controller]")]
    public class WordsController : Controller
    {
        private readonly IWordsService _wordsService;

        public WordsController(IWordsService wordsService)
        {
            _wordsService = wordsService;
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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
