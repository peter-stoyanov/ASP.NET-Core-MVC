using LanguageBuilder.Services.Models.UsersSearch;
using System.Collections.Generic;
using System.ComponentModel;

namespace LanguageBuilder.Web.Areas.Admin.Models
{
    public class UsersSearchFormViewModel
    {
        [DisplayName("Keywords")]
        public string Keywords { get; set; }

        [DisplayName("Page size")]
        public int RowCount { get; set; }

        public List<int> RowCounts = new List<int>() { 10, 20, 50, 100 };

        public UsersSearchFormViewModel()
        {
            this.RowCount = 15;
        }

        public Request ToSearchRequest()
        {
            return new Request()
            {
                PageSize = this.RowCount,
                Keywords = this.Keywords
            };
        }
    }
}
