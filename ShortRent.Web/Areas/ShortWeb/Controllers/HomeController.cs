using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShortRent.WebCore.MVC;

namespace ShortRent.Web.Areas.ShortWeb.Controllers
{
    public class HomeController : BaseController
    {
        // GET: ShortWeb/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}