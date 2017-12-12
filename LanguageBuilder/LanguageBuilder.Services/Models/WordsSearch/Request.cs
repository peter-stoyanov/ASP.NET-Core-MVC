using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Models.Search;
using System.Collections.Generic;

namespace LanguageBuilder.Services.Models.WordsSearch
{
    public class Request : BaseRequest<Word>
    {
        public string Keywords { get; set; }
        public List<int> LanguageIds { get; set; } = new List<int>();
    }
}
