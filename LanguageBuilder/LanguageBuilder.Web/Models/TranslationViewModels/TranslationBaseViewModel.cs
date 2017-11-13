using LanguageBuilder.Data.Models;
using LanguageBuilder.Web.Models.WordViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageBuilder.Web.Models.TranslationViewModels
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
