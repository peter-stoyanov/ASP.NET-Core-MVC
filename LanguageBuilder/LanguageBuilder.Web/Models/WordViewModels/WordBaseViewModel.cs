using LanguageBuilder.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageBuilder.Web.Models.WordViewModels
{
    public class WordBaseViewModel
    {
        [StringLength(15)]
        public string Gender { get; set; }

        [Display(Name = "Syntax type")]
        [StringLength(15)]
        public string SyntaxType { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 1)]
        public string Content { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }

        //public bool IsDeleted { get; set; }

        public List<UserWord> Users { get; set; } = new List<UserWord>();

        public List<Example> Examples { get; set; } = new List<Example>();

        public string Notes { get; set; }

        public List<Translation> SourceTranslations { get; set; } = new List<Translation>();
        public List<Translation> TargetTranslations { get; set; } = new List<Translation>();
    }
}
