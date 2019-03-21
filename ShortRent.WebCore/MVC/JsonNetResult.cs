using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json.Serialization;

namespace ShortRent.WebCore.MVC
{
    /// <summary>
    /// 这是继承jsonResult的类重写用newtonsoft.json
    /// </summary>
    public class JsonNetResult:JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
           if(context==null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            var response = context.HttpContext.Response;
            //如果用户设置过ContentType的话用用户的如果没有就用默认json的
            response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/json";
            if(ContentEncoding!=null)
            {
                response.ContentEncoding = ContentEncoding;
            }
            var jsonSerializerSettings = new JsonSerializerSettings();
            //设置
            //首字母小写
            jsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //日期格式
            jsonSerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            var json=JsonConvert.SerializeObject(Data,Formatting.None,jsonSerializerSettings);
            response.Write(json);
        }
    }
}
