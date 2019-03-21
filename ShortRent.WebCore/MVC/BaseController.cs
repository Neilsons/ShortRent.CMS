using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ShortRent.WebCore.MVC
{
    public class BaseController:Controller
    {
        /// <summary>
        /// 替换微软的JsonResult
        /// </summary>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="contentEncoding"></param>
        /// <returns></returns>
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding,JsonRequestBehavior behavior)
        {
          return new JsonNetResult() { Data=data,ContentEncoding=contentEncoding,ContentType=contentType,JsonRequestBehavior=behavior};
        }
    }
}
