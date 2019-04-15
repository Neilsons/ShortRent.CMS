using ShortRent.Core.Infrastructure;
using ShortRent.Core.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ShortRent.Resource;
using System.Reflection;

namespace ShortRent.WebCore.MVC
{
    public class BaseController:Controller
    {
        public BaseController()
        {
            //GlobalManager.languages = ServiceContainer.Resolve<ILanguages>();
        }
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
        /// <summary>
        /// 返回创建的对象的信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Title"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected string GetDescription<T>(string Title, T model) where T : class
        {
            Type type = typeof(T);
            StringBuilder sBuild = new StringBuilder();
            sBuild.Append(Title);
            sBuild.Append("=>(");
            foreach (var property in type.GetProperties())
            {
                string key = type.Name + property.Name;
                sBuild.Append(ResourceManagers.getHumentData(key));
                sBuild.Append(" : ");
                sBuild.Append(property.GetValue(model));
                sBuild.Append(" ; ");
            }
            sBuild.Append(")");
            return sBuild.ToString();

        }
        protected string GetDescription<T>(string Title, T model,T oldModel) where T : class
        {
            Type type = typeof(T);
            StringBuilder sBuild = new StringBuilder();
            sBuild.Append(Title);
            sBuild.Append("=>(");
            PropertyInfo pro = type.GetProperty("ID");
            string Idkey = type.Name + pro.Name;
            sBuild.Append(ResourceManagers.getHumentData(Idkey));
            sBuild.Append(" : ");
            sBuild.Append(pro.GetValue(model));
            foreach (var property in type.GetProperties())
            {
                if (property.Name == "ID")
                    continue;
                sBuild.Append(" ; ");
                string key = type.Name + property.Name;
                sBuild.Append(ResourceManagers.getHumentData(key));
                sBuild.Append("=>(编辑之前："); 
                sBuild.Append(property.GetValue(oldModel));
                sBuild.Append(" ; ");
                sBuild.Append("编辑之后: ");
                sBuild.Append(property.GetValue(model));
                sBuild.Append(" ; )");

            }
            sBuild.Append(")");
            return sBuild.ToString();

        }
        //protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        //{
        //    string abbreviation = RouteData.Values["lg"].ToString();
        //    GlobalManager.languages.Current = GlobalManager.languages[abbreviation];
        //    return base.BeginExecuteCore(callback, state);
        //}
    }
}
