using LanguageBuilder.Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LanguageBuilder.Web.ViewModels.UserViewModels
{
    public class UserEditProfileViewModel : UserProfileViewModel
    {
        [Required]
        public string Email { get; set; }

        //[Required]
        //public int SubscriptionId { get; set; }
        //public Subscription Subscription { get; set; }

        public List<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    }
}
