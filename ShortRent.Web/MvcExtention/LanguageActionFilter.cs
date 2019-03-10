using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ShortRent.Web
{
    /// <summary>
    /// 过滤要使用的语言
    /// </summary>
    public class LanguageActionFilter:IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            string lg = filterContext.RouteData.Values["lg"].ToString();
            //设置语言文化
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lg);
            Thread.CurrentThread.CurrentCulture = new CultureInfo(lg);
        }
    }
}