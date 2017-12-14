using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Text;

namespace LanguageBuilder.Web.Helpers.TagHelpers
{
    [HtmlTargetElement("card-accordion")]
    public class CardAccordionTagHelper : TagHelper
    {
        [HtmlAttributeName("title")]
        public string Title { get; set; }

        [HtmlAttributeName("body")]
        public string Body { get; set; }

        [HtmlAttributeName("image")]
        public string Image { get; set; }

        [HtmlAttributeName("container-class")]
        public string ContainerClass { get; set; }

        [HtmlAttributeName("image-class")]
        public string ImageClass { get; set; }

        [HtmlAttributeName("body-as-html")]
        public bool BodyAsHtml { get; set; }

        [HtmlAttributeName("title-as-html")]
        public bool TitleAsHtml { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (string.IsNullOrEmpty(Title) && string.IsNullOrEmpty(Body)) { return; }

            string titleText = TitleAsHtml ? Title : System.Net.WebUtility.HtmlEncode(Title);
            string bodyText = BodyAsHtml ? Body : System.Net.WebUtility.HtmlEncode(Body);
            string image = $@"<img src=""{Image}"" class=""{(String.IsNullOrEmpty(ImageClass) ? "" : ImageClass)}"" >";
            string id = context.UniqueId;
            string headerId = $"heading{id}";
            string collapseId = $"collapse{id}";

            StringBuilder sb = new StringBuilder();

            sb.Append(
            $@"<div class=""card"">
                <div class=""card-header"" id=""{headerId}"" role=""tab"">
                    <h6 class=""mb-0"">
                        <a aria-controls=""{collapseId}"" aria-expanded=""true"" data-toggle=""collapse"" href=""#{collapseId}"">{titleText}</a>
                    </h6>
                </div>
                <div aria-labelledby= ""{headerId}"" class=""collapse"" data-parent=""#accordion"" id=""{collapseId}"" role=""tabpanel"">
                    <div class=""card-body"">
                        <p>{(String.IsNullOrEmpty(Image) ? bodyText : image)}</p>
                    </ div>
                </div>
            </div>");

            output.TagName = "div";

            if (!String.IsNullOrEmpty(ContainerClass))
            {
                output.Attributes.Add("class", ContainerClass);
            }

            output.Content.SetHtmlContent(sb.ToString());
        }
    }
}
