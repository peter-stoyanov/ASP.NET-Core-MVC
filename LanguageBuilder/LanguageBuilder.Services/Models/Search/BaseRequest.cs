using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;

namespace LanguageBuilder.Services.Models.Search
{
    public abstract class BaseRequest<T>
    {
        //public string SortColumn { get; set; }
        //Expression<Func<T>> SortColumn { get; set; }
        public bool SortDesc { get; set; }

        //public int? Offset { get; set; }
        public int PageSize { get; set; } = 50;
        public int PageNumber { get; set; } = 1;
        public CancellationToken CancellationToken { get; set; }
         

        public bool ReturnTotalRecords { get; set; } = true;
    }
}
