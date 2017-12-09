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
        public int TotalItems { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public PaginationSettings Paging { get; set; }

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
                    this.TotalItems = value.Records.TotalItems;
                    this.PageSize = value.Records.PageSize;
                    this.PageNumber = value.Records.PageNumber;
                }
            }
        }
    }
}
