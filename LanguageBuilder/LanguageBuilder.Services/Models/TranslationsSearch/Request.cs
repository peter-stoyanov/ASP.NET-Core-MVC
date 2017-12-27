using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Models.Search;
using System.Collections.Generic;

namespace LanguageBuilder.Services.Models.TranslationsSearch
{
    public class Request : BaseRequest<Translation>
    {
        public string Keywords { get; set; }
        public List<int> SourceLanguageIds { get; set; } = new List<int>();
        public List<int> TargetLanguageIds { get; set; } = new List<int>();
    }
}
