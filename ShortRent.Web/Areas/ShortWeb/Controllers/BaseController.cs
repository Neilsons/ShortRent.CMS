using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShortRent.Core.Domain;
using ShortRent.WebCore.MVC;

namespace ShortRent.Web.Areas.ShortWeb.Controllers
{
    public class BaseController:Controller
    {
        //返回当前的用户信息
        public PersonUserType Current
        {
            get
            {
                WorkContext work = new WorkContext();
                return work.CurrentWebPerson;
            }
        }
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult() { Data = data, ContentEncoding = contentEncoding, ContentType = contentType, JsonRequestBehavior = behavior };
        }
    }
}