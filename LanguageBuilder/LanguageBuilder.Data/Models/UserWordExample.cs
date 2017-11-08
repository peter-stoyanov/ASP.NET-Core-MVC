using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageBuilder.Data.Models
{
    public class UserWordExample
    {
        public int UserWordId { get; set; }
        public UserWord UserWord { get; set; }

        public int ExampleId { get; set; }
        public Example Example { get; set; }

    }
}
