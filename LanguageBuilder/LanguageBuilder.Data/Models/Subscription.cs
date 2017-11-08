using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LanguageBuilder.Data.Models
{
    public class Subscription
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public List<User> Users { get; set; } = new List<User>();
    }
}
