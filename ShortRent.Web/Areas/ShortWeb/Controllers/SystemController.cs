using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShortRent.WebCore.MVC;

namespace ShortRent.Web.Areas.ShortWeb.Controllers
{
    public class SystemController : BaseController
    {
        // GET: ShortWeb/System
        public ActionResult InternalServerError()
        {
            return View();
        }
        public ActionResult NotFound()
        {
            return View();
        }
    }
}