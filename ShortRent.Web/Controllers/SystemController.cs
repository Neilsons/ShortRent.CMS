using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShortRent.WebCore.MVC;

namespace ShortRent.Web.Controllers
{
    public class SystemController : BaseController
    {
        // GET: System
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