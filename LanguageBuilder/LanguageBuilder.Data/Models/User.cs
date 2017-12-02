using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace LanguageBuilder.Data.Models
{
    public class User : IdentityUser<string>
    {
        public bool IsActive { get; set; }

        public string Name { get; set; }

        public DateTime? Birthdate { get; set; }

        public List<UserLanguage> Languages { get; set; } = new List<UserLanguage>();

        public List<UserWord> Words { get; set; } = new List<UserWord>();

        public int SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }
    }
}
