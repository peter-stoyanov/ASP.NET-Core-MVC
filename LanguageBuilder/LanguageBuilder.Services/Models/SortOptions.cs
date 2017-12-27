using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageBuilder.Services.Models
{
    public class SortOptions
    {
        public string SortColumn { get; set; } = String.Empty;

        public SortDirection SortDirection { get; set; } = SortDirection.Ascending;
    }
}
