using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static LanguageBuilder.Data.DataConstants;

namespace LanguageBuilder.Data.Models
{
    public class Subscription : BaseEntity<int>
    {
        [Required]
        [MaxLength(SUBSCRIPTION_MAX_LENGTH)]
        public string Name { get; set; }

        public List<User> Users { get; set; } = new List<User>();
    }
}
