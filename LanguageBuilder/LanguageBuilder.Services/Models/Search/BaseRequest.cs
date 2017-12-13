using System.Threading;

namespace LanguageBuilder.Services.Models.Search
{
    public abstract class BaseRequest<T>
    {
        public bool SortDesc { get; set; }

        public int PageSize { get; set; } = 50;

        public int PageNumber { get; set; } = 1;

        public CancellationToken CancellationToken { get; set; }

        public bool ReturnTotalRecords { get; set; } = true;
    }
}
