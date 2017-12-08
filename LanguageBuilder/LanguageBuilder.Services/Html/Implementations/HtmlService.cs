using Ganss.XSS;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageBuilder.Services.Html.Implementations
{
    public class HtmlService : IHtmlService
    {
        private readonly HtmlSanitizer htmlSanitizer;

        public HtmlService()
        {
            this.htmlSanitizer = new HtmlSanitizer();
            this.htmlSanitizer.AllowedAttributes.Add("class");
        }

        public string Sanitize(string htmlContent)
            => this.htmlSanitizer.Sanitize(htmlContent);
    }
}
