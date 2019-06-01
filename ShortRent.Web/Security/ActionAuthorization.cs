using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ShortRent.Web
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method,Inherited =false,AllowMultiple =false)]
    public class ActionAuthorization:AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentException(nameof(filterContext));
            }
            if(filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute),true))
            {
                return;
            }
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                HandleUnauthorizedRequest(filterContext);
                return;
            }
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if(filterContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.Result = new HttpStatusCodeResult((int)HttpStatusCode.Forbidden);
            }
            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}