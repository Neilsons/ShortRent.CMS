using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Models
{
    public class PublishTagsHumanModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 标签名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 标签颜色
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// 标签排序
        /// </summary>
        public int TagOrder { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}