using cloudscribe.Web.Pagination;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Models.WordsSearch;
using System.Collections.Generic;

namespace LanguageBuilder.Web.ViewModels.WordViewModels
{
    public class WordsSearchViewModel
    {
        private Response _response = new Response();

        public WordsSearchFormViewModel SearchForm { get; set; }

        public List<Word> Data { get; set; }

        public char SelectedLetter { get; set; }

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
