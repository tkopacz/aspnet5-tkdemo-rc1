using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using Microsoft.AspNet.Razor.TagHelpers;
using Microsoft.Extensions.WebEncoders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TKDemoMVC.TagHelpers
{
    [HtmlTargetElement(Attributes = "demo-repeat")]
    public class RepeatTagHelper : TagHelper {
        [HtmlAttributeName("count")]
        public int Count { get; set; }
        private IHtmlEncoder encoder;

        public RepeatTagHelper(IHtmlEncoder encoder)
        {
            this.encoder = encoder;
        }
        public override async void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Count <= 0)
            {
                return;
            }
            var b = await output.GetChildContentAsync();
            for (int i = 0; i < Count; i++)
            {
                output.Content.Append(b);
            }
            output.PostElement.AppendHtml($"<p>{Count}</p>");
        }
    }
}
