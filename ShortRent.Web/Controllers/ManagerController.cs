using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShortRent.WebCore.MVC;

namespace ShortRent.Web.Controllers
{
    public class ManagerController : BaseController
    {
        public ActionResult List()
        {
            ViewBag.Title = "系统管理";
            ViewBag.Content = "菜单列表";
            return View();
        }
    }
}