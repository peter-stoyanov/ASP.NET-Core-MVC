using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageBuilder.Data.Models
{
    public class Translation
    {
        public int SourceWordId { get; set; }
        public Word SourceWord { get; set; }

        public int TargetWordId { get; set; }
        public Word TargetWord { get; set; }
    }
}
