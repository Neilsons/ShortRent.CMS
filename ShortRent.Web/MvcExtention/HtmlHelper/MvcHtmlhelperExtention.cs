using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShortRent.Web.MvcExtention
{
    public static class MvcHtmlhelperExtention
    {
        public static MvcHtmlString ActionLinkAdd(this HtmlHelper htmlHelper, string linkText,string action,object HtmlAttribute)
        {
            string FontIcon = "fa fa-plus";
            RouteValueDictionary attribute = HtmlHelper.AnonymousObjectToHtmlAttributes(HtmlAttribute);
            return ActionLinkAny(htmlHelper,linkText,action,FontIcon, attribute);
        }
        public  static MvcHtmlString ActionLinkAny(this HtmlHelper htmlHelper, string linkText, string action,string FontIcon, IDictionary<string, object> HtmlAttribute)
        {
            return ActionLinkAny(htmlHelper, linkText,action,null,FontIcon, null, HtmlAttribute);
        }
        public static MvcHtmlString ActionLinkAny(this HtmlHelper htmlHelper, string linkText,string action,string controller,string FontIcon, object roudaValues, IDictionary<string,object> HtmlAttribute)
        {
            if(string.IsNullOrEmpty(controller))
            {
                controller = htmlHelper.ViewContext.RouteData.Values["controller"].ToString();
            }
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            TagBuilder iHtml = new TagBuilder("i");
            iHtml.AddCssClass(FontIcon);
            StringBuilder sbulider = new StringBuilder();
            sbulider.Append(iHtml.ToString());
           
            TagBuilder spanHtml = new TagBuilder("span")
            {
                InnerHtml = "&nbsp;&nbsp;" + linkText
            };
            sbulider.Append(spanHtml.ToString());
            TagBuilder aHtml = new TagBuilder("a")
            {
                InnerHtml = sbulider.ToString()
            };
            string url = urlHelper.Action(action,controller,roudaValues);
            aHtml.MergeAttribute("href", url);
            aHtml.MergeAttributes(HtmlAttribute);
            return MvcHtmlString.Create(aHtml.ToString());        
        }
    }
}