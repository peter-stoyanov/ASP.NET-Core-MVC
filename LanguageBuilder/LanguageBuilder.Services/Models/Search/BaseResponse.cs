using cloudscribe.Pagination.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageBuilder.Services.Models.Search
{
    public abstract class BaseResponse<T> where T : class
    {
        public PagedResult<T> Records { get; set; }
        //public long TotalRecords { get; set; }
    }
}
