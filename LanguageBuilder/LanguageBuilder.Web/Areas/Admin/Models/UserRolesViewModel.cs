﻿using LanguageBuilder.Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LanguageBuilder.Web.Areas.Admin.Models
{
    public class UserRolesViewModel
    {
        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public List<string> SelectedRoles { get; set; }

        public List<Role> Roles { get; set; }

        public string Caller { get; set; }
    }
}
