using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageBuilder.Web.ViewModels
{
    public class SortOptions
    {
        string SortColumn { get; set; }

        SortDirection SortDirection { get; set; } = SortDirection.Ascending;
    }
}
