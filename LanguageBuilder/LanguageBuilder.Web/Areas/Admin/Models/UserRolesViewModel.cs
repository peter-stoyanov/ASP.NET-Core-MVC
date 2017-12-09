using cloudscribe.Web.Pagination;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Models.UsersSearch;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LanguageBuilder.Web.Areas.Admin.Models
{
    public class UserRolesViewModel
    {
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }
           
        // at least 1 role ?
        public List<string> SelectedRoles { get; set; }

        public List<Role> Roles { get; set; }

        public string Caller { get; set; }
    }
}
