using LanguageBuilder.Data.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LanguageBuilder.Web.Areas.Admin.Models
{
    public class WordsUploadViewModel
    {
        [Required]
        public int LanguageId { get; set; }

        public List<Language> Languages { get; set; }

        [Required]
        public IFormFile File { get; set; }
    }
}
