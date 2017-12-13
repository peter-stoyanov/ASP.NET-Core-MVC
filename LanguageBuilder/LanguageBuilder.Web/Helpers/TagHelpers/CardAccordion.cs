using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageBuilder.Web.Helpers.TagHelpers
{
    public class CardAccordionTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string title = String.Empty;
            string body = String.Empty;
            string id = context.UniqueId;
            string headerId = $"heading{id}";
            string collapseId = $"collapse{id}";

            if (output.Attributes.ContainsName("title"))
            {
                title = output.Attributes["title"].Value.ToString();
            };

            if (output.Attributes.ContainsName("body"))
            {
                title = output.Attributes["body"].Value.ToString();
            };

            output.Content.SetContent(
            $@"div class=""card"">
                <div class=""card-header"" id=""{headerId}"" role=""tab"">
                    <h6 class=""mb-0"">
                        <a aria-controls=""{collapseId}"" aria-expanded=""true"" data-toggle=""collapse"" href=""#{collapseId}"">{title}</a>
                    </h6>
                </div>
                <div aria-labelledby= ""{headerId}"" class=""collapse"" data-parent=""#accordion"" id=""{collapseId}"" role=""tabpanel"">
                    <div class=""card-body"">
                        <p>{body}</p>
                    </div>
                </div>
            </div>");


            output.Attributes.Remove(context.AllAttributes["title"]);
            output.Attributes.Remove(context.AllAttributes["body"]);
        }
    }
}


