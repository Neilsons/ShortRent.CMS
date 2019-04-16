using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Models
{
    public class LogInfoException
    {
        /// <summary>
        /// 错误类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 报错信息
        /// </summary>
        public string Message { get; set; }
        //这些键/值对提供有关异常的其他用户定义信息。
        public string Date { get; set; }
        /// <summary>
        /// 获取抛出当前异常的方法。
        /// </summary>
        public string TargetSite { get; set; }
        /// <summary>
        /// 获取调用堆栈上直接帧的字符串表示形式。
        /// </summary>
        public string StackTrace { get; set; }
        /// <summary>
        /// 应用程序的名称或导致错误的对象。
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 分配给特定异常的编码数值。
        /// </summary>
        public string HResult { get; set; }
    }
}