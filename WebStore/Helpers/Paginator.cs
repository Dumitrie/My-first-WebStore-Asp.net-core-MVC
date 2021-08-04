using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using WebStore.Models;

namespace WebStore.Helpers
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class Paginator : TagHelper
    {
        private readonly IUrlHelperFactory _urlHelperFactory;

        public Paginator(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public PaginatorInfo PageModel { get; set; }

        public string PageAction { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
            var tagUl = new TagBuilder("ul"); // <div> </div>
            tagUl.AddCssClass("pagination");
            /*
             *  <a href="/ProductView/?page=1"> 1 </a>
                <a href="/ProductView/?page=2"> 2 </a>
                <a href="/ProductView/?page=3"> 3 </a>
             */
            if (PageModel.PagesCount > 1)
            {
                for (int i = 1; i <= PageModel.PagesCount; i++)
                {
                    var tagLi = new TagBuilder("li");
                    tagLi.AddCssClass("page-item");

                    var tagA = new TagBuilder("a"); // <a> </a>

                    // href="/ProductView/?page=1"
                    tagA.Attributes["href"] = urlHelper.Action(PageAction, new { pageNumber = i });
                    tagA.InnerHtml.Append(i.ToString()); // 1
                    tagA.AddCssClass("page-link");

                    if (i == PageModel.CurrentPage)
                    {
                        tagLi.AddCssClass("active");
                    }

                    tagLi.InnerHtml.AppendHtml(tagA);

                    tagUl.InnerHtml.AppendHtml(tagLi);
                }
            }
            

            output.Content.AppendHtml(tagUl);
        }
    }
}
