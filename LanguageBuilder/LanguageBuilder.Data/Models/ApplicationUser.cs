﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LanguageBuilder.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public bool IsActive { get; set; }

        public List<UserLanguage> Languages { get; set; } = new List<UserLanguage>();

        public List<UserWord> Words { get; set; } = new List<UserWord>();

        public int SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }
    }
}
