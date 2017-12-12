using LanguageBuilder.Data.Models;
using LanguageBuilder.Web.ViewModels.WordViewModels;
using System.Collections.Generic;

namespace LanguageBuilder.Web.DTOs
{
    public class TranslationDTO
    {
        public int SourceWordId { get; set; }
        public string SourceWord { get; set; }

        public int TargetWordId { get; set; }
        public string TargetWord { get; set; }

    }
}
