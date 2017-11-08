using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LanguageBuilder.Data.Models
{
    public class UserLanguage
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
