using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LanguageBuilder.Data.Models
{
    public class Subscription : BaseEntity<int>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public List<User> Users { get; set; } = new List<User>();
    }
}
