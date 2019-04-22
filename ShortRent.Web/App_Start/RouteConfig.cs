using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShortRent.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Language",
            //    url: "{lg}/{controller}/{action}/{id}",
            //    defaults: new { lg="zh",controller = "Person", action = "Index", id = UrlParameter.Optional },
            //    constraints: new { lg="[a-zA-Z]{2}"}  
            //).DataTokens["language"]=true;
            //routes.MapRoute(
            //    name:"Default",
            //    url:"{controller}/{action}/{id}",
            //    defaults: new { lg = "zh", controller = "Person", action = "Index", id = UrlParameter.Optional },
            //    constraints:new { lg="zh"}
            //    ).DataTokens["language"] = false;
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Person", action = "Login", id = UrlParameter.Optional }
                );
        }
    }
}
