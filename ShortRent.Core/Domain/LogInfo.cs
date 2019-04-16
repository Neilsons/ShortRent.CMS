using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ShortRent.Core.Domain
{
    public class LogInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public long ID { get; set; }
        public string Message { get; set; }
        /// <summary>
        /// 进程名称
        /// </summary>
        public string ProcessName { get; set; }
        /// <summary>
        /// 进程id
        /// </summary>
        public int? ProcessId { get; set; }
        /// <summary>
        /// 线程ID
        /// </summary>
        public int? ThreadId { get; set; }
        /// <summary>
        /// 机器名
        /// </summary>
        public string MachineName{get;set;}
        /// <summary>
        /// 分类目录
        /// </summary>
        public string Catalogue { get; set; }
        //堆栈跟踪
        public string StachTrace { get; set; }
        /// <summary>
        /// 方法全名
        /// </summary>
        public string MethodFullName { get; set; }
        /// <summary>
        /// 异常信息详情
        /// </summary>
        public string Exception { get; set; }
        public DateTime CreateTime { get; set; }
    }
}