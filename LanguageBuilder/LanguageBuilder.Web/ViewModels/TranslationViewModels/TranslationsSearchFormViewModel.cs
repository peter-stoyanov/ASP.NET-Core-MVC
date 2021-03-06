﻿using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Models;
using LanguageBuilder.Services.Models.TranslationsSearch;
using System.Collections.Generic;
using System.ComponentModel;

namespace LanguageBuilder.Web.ViewModels.TranslationViewModels
{
    public class TranslationsSearchFormViewModel
    {
        [DisplayName("Keywords")]
        public string Keywords { get; set; }

        [DisplayName("Source Language")]
        public int SourceLanguageId { get; set; } = 1;

        [DisplayName("Target Language")]
        public int TargetLanguageId { get; set; } = 2;

        public int Page { get; set; } = 1;

        public SortOptions SortOptions { get; set; } = new SortOptions();

        public List<Language> Languages { get; set; }

        [DisplayName("Page size")]
        public int RowCount { get; set; }

        public List<int> RowCounts = new List<int>() { 50, 100, 200, 300, 500, 1000 };

        public TranslationsSearchFormViewModel()
        {
            this.RowCount = 15;
        }

        public Request ToSearchRequest()
        {
            return new Request()
            {
                PageSize = this.RowCount,
                PageNumber = Page,
                SourceLanguageIds = new List<int>() { this.SourceLanguageId },
                TargetLanguageIds = new List<int>() { this.TargetLanguageId },
                Keywords = this.Keywords
            };
        }
    }
}
