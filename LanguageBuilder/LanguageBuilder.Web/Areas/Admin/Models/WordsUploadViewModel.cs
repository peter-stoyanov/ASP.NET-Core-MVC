using LanguageBuilder.Data.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace LanguageBuilder.Web.Areas.Admin.Models
{
    public class WordsUploadViewModel
    {
        public int LanguageId { get; set; }
        public List<Language> Languages { get; set; }

        public bool CheckForDuplicates { get; set; }

        public IFormFile File { get; set; }

        public string TempStringData { get; set; }
    }
}
