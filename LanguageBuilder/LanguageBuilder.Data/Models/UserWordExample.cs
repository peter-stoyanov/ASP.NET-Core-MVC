namespace LanguageBuilder.Data.Models
{
    public class UserWordExample : BaseEntity<int>
    {
        public int UserWordId { get; set; }
        public UserWord UserWord { get; set; }

        public int ExampleId { get; set; }
        public Example Example { get; set; }
    }
}
