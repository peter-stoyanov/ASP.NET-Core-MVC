using LanguageBuilder.Data.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LanguageBuilder.Web.Areas.Admin.Models
{
    public class TranslationsUploadViewModel
    {
        [Required]
        public int FromLanguageId { get; set; }

        [Required]
        public int ToLanguageId { get; set; }

        public List<Language> Languages { get; set; }

        [Required]
        public IFormFile File { get; set; }
    }
}
