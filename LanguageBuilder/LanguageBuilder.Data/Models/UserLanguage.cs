using System.ComponentModel.DataAnnotations;

namespace LanguageBuilder.Data.Models
{
    public class UserLanguage : BaseEntity<int>
    {
        //public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
