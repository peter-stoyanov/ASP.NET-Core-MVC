using cloudscribe.Web.Pagination;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Models.UsersSearch;
using System.Collections.Generic;

namespace LanguageBuilder.Web.Areas.Admin.Models
{
    public class LanguageListingViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int WordsCount { get; set; }
    }
}
