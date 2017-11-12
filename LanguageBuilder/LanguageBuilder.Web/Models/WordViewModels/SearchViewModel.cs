using LanguageBuilder.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageBuilder.Web.Models.WordViewModels
{
    public class SearchViewModel
    {
        public IEnumerable<Word> Words { get; set; } = new List<Word>();
        
        //public string Content { get; set; }

        //public int LanguageId { get; set; }
        //public Language Language { get; set; }

        //public int Level { get; set; }
    }
}
