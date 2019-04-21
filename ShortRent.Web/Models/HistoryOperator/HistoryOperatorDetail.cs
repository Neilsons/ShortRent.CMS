using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Models
{
    public class HistoryOperatorDetail
    {
        /// <summary>
        /// 操作的模块名称
        /// </summary>
        public string EntityModule { get; set; }
        /// <summary>
        /// 具体的操作
        /// </summary>
        public string Operates { get; set; }
        /// <summary>
        /// 详细描述
        /// </summary>
        public string DetailDescirption { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// person表中的外键
        /// </summary>
        public string  pName { get; set; }
    }
}