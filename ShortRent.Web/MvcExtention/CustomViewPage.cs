using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShortRent.Resource;

namespace ShortRent.Web.MvcExtention
{
    /// <summary>
    /// 自定义继承自WebViewPage的类 ,这里将 <pages pageBaseType="System.Web.Mvc.WebViewPage">替换掉
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class CustomViewPage<TModel>:WebViewPage<TModel>
    {
        public string T(string key)
        {
            return ResourceManagers.getViewElement(key);
        }
    }
}