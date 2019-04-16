using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Models
{
    public class LogDetailViewModel
    {
        /// <summary>
        /// 堆栈跟踪信息
        /// </summary>
        public string StachTrace { get; set; }
        public LogInfoException Exception { get; set; }
    }
}