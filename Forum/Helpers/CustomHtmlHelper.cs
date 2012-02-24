using System.Web.Mvc;

namespace Forum.Helpers
{
    public static class CustomHtmlHelper
    {
        public static MvcHtmlString ImageLink(this HtmlHelper htmlHelper, string imgSrc, string alt, string actionName, string controllerName, object routeValues, object htmlAttributes, object imgHtmlAttributes)
        {
            var urlHelper = ((Controller)htmlHelper.ViewContext.Controller).Url;
            var imgTag = new TagBuilder("img");
            imgTag.MergeAttribute("src", imgSrc);
            imgTag.MergeAttribute("alt", alt);
            imgTag.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(imgHtmlAttributes), true);
            var url = urlHelper.Action(actionName, controllerName, routeValues);

            var imglink = new TagBuilder("a");
            imglink.MergeAttribute("href", url);
            imglink.InnerHtml = imgTag.ToString();
            imglink.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), true);

            return MvcHtmlString.Create(imglink.ToString());
        }
    }
}