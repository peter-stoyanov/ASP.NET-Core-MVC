using LanguageBuilder.Data.Models;
using System.Collections.Generic;

namespace LanguageBuilder.Web.ViewModels.WordViewModels
{
    public class SearchViewModel
    {
        public IEnumerable<Word> Words { get; set; } = new List<Word>();

        // add paging
    }
}
