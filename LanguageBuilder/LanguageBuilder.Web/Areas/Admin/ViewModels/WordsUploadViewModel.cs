using LanguageBuilder.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageBuilder.Web.Areas.Admin.ViewModels
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
