using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShortRent.WebCore.MVC;

namespace ShortRent.Web.Controllers
{
    public class RoleController : BaseController
    {
        #region Contruction
        public RoleController()
        {
            
        }
        #endregion
        // GET: Role
        public ActionResult Index()
        {

            return View();
        }
    }
}