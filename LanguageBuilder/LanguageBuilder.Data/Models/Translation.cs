namespace LanguageBuilder.Data.Models
{
    public class Translation : BaseEntity<int>
    {
        public int SourceWordId { get; set; }
        public Word SourceWord { get; set; }

        public int TargetWordId { get; set; }
        public Word TargetWord { get; set; }
    }
}
