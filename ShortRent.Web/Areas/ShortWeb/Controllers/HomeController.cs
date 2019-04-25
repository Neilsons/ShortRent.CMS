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
        #region Methods
        public ActionResult List()
        {

            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        #endregion
        // GET: ShortWeb/Home
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

    }
}