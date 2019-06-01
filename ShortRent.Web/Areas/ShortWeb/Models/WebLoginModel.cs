using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Areas.ShortWeb.Models
{
    public class WebLoginModel
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// 招聘者或者被招聘者
        /// </summary>
        public bool Type { get; set; }
        /// <summary>
        /// 记住我
        /// </summary>
        public bool ReadMe { get; set; }

    }
}