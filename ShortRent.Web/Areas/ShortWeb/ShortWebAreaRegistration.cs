using System.Web.Mvc;

namespace ShortRent.Web.Areas.ShortWeb
{
    public class ShortWebAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ShortWeb";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ShortWeb_default",
                "ShortWeb/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}