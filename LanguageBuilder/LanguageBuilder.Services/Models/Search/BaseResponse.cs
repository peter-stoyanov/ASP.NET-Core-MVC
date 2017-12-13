using cloudscribe.Pagination.Models;

namespace LanguageBuilder.Services.Models.Search
{
    public abstract class BaseResponse<T> where T : class
    {
        public PagedResult<T> Records { get; set; }

        public long TotalRecords { get; set; }
    }
}
