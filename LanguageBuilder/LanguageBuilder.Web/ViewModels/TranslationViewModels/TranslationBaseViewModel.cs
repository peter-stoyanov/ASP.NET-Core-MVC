using LanguageBuilder.Data.Models;
using LanguageBuilder.Web.ViewModels.WordViewModels;
using System.Collections.Generic;

namespace LanguageBuilder.Web.ViewModels.TranslationViewModels
{
    public class TranslationBaseViewModel
    {
        public int SourceWordId { get; set; }
        public WordCreateViewModel SourceWord { get; set; } = new WordCreateViewModel();

        public int TargetWordId { get; set; }
        public WordCreateViewModel TargetWord { get; set; } = new WordCreateViewModel();

        public Example Example { get; set; }

        public IEnumerable<Language> Languages { get; set; } = new List<Language>();
    }
}
