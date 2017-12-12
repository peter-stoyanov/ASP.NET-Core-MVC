using LanguageBuilder.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LanguageBuilder.Web.ViewModels.UserViewModels
{
    public class UserProfileViewModel
    {
        [Required]
        public string Id { get; set; }

        public string Name { get; set; }

        [Required]
        public string Username { get; set; }

        public DateTime? Birthdate { get; set; }

        public List<Language> Languages { get; set; } = new List<Language>();
    }
}
