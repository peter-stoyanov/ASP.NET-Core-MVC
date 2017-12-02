using LanguageBuilder.Data.Models;
using System.Collections.Generic;

namespace LanguageBuilder.Web.Models.WordViewModels
{
    public class SearchViewModel
    {
        public IEnumerable<Word> Words { get; set; } = new List<Word>();

        // add paging
    }
}
