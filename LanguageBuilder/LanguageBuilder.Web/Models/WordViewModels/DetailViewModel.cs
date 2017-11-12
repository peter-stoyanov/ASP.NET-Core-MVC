using LanguageBuilder.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageBuilder.Web.Models.WordViewModels
{
    public class DetailViewModel
    {
        public string Content { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }

        public int Level { get; set; }

        public int Id { get; set; }

        public string Gender { get; set; }

        public string SyntaxType { get; set; }

        public List<Example> Examples { get; set; } = new List<Example>();
    }
}
