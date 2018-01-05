using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Models;
using LanguageBuilder.Services.Models.WordsSearch;
using System.Collections.Generic;
using System.ComponentModel;

namespace LanguageBuilder.Web.ViewModels.WordViewModels
{
    public class WordsSearchFormViewModel
    {
        [DisplayName("Keywords")]
        public string Keywords { get; set; }

        [DisplayName("Language")]
        public int LanguageId { get; set; } = 1;

        public List<Language> Languages { get; set; }

        public string SelectedLetter { get; set; } = "";

        public SortOptions SortOptions { get; set; } = new SortOptions();

        [DisplayName("Page size")]
        public int RowCount { get; set; }

        public List<int> RowCounts = new List<int>() { 50, 100, 200, 300, 500, 1000 };

        public WordsSearchFormViewModel()
        {
            this.RowCount = 15;
        }

        public Request ToSearchRequest()
        {
            return new Request()
            {
                PageSize = this.RowCount,
                LanguageIds = new List<int>() { this.LanguageId },
                Keywords = this.Keywords
            };
        }
    }
}