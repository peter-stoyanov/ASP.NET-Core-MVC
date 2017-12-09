using cloudscribe.Web.Pagination;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Models.UsersSearch;
using System.Collections.Generic;

namespace LanguageBuilder.Web.Areas.Admin.Models
{
    public class UsersSearchViewModel
    {
        private Response _response = new Response();

        public UsersSearchFormViewModel SearchForm { get; set; }

        public List<User> Data { get; set; }

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
                    this.Paging.MaxPagerItems = 100; // value.Records.TotalItems
                }
            }
        }
    }
}
