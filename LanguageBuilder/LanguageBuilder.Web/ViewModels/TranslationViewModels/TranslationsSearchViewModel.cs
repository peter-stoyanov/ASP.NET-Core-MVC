using cloudscribe.Web.Pagination;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Models.TranslationsSearch;
using System.Collections.Generic;

namespace LanguageBuilder.Web.ViewModels.TranslationViewModels
{
    public class TranslationsSearchViewModel
    {
        private Response _response = new Response();

        public TranslationsSearchFormViewModel SearchForm { get; set; }

        public List<Translation> Data { get; set; }

        public PaginationSettings Paging { get; set; } = new PaginationSettings();

        public Response Response
        {
            get
            {
                return this._response;
            }
            set
            {
                if (value != null && value.Records != null)
                {
                    this.Data = value.Records.Data;
                    this.Paging.CurrentPage = value.Records.PageNumber;
                    this.Paging.TotalItems = value.Records.TotalItems;
                    this.Paging.ItemsPerPage = value.Records.PageSize;
                    this.Paging.MaxPagerItems = value.Records.TotalItems;
                }
            }
        }
    }
}
