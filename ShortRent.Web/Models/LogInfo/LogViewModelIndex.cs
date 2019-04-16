using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Models
{
    public class LogViewModelIndex
    {
        /// <summary>
        /// 编号
        /// </summary>
        public long ID { get; set; }
        //错误信息
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
        public string MachineName { get; set; }
        /// <summary>
        /// 分类目录
        /// </summary>
        public string Catalogue { get; set; }
        /// <summary>
        /// 方法全名
        /// </summary>
        public string MethodFullName { get; set; }
        public DateTime CreateTime { get; set; }
    }
}