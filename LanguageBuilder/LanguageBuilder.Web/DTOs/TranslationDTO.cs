namespace LanguageBuilder.Web.DTOs
{
    public class TranslationDTO
    {
        public int SourceWordId { get; set; }
        public string SourceWord { get; set; }

        public int TargetWordId { get; set; }
        public string TargetWord { get; set; }
    }
}
