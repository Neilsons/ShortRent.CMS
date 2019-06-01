using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Models
{
    public class BussinessIndex
    {
        public int ID { get; set; }
        /// <summary>
        ///名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}