using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ShortRent.Web.MvcExtention
{
    public static class MvcHtmlhelperExtention
    {
        public static MvcHtmlString ActionLinkAny(this HtmlHelper htmlHelper, string linkText,string FontIcon,object HtmlAttribute)
        {
            string action = htmlHelper.ViewContext.RouteData.Values["action"].ToString();
            string Controller = htmlHelper.ViewContext.RouteData.Values["controller"].ToString();
            StringBuilder sbuilder = new StringBuilder();
            sbuilder.Append("<a href=\"");
            sbuilder.Append(Controller);
            sbuilder.Append("/");
            sbuilder.Append(action);
            return MvcHtmlString.Create(sbuilder.ToString());
        }
    }
}