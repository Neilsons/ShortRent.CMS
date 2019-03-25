using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Core.Domain
{
    public  class CompanyPerTag
    {
        /// <summary>
        /// 标签名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 标签颜色
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// 0代表普通用户 1代表招聘
        /// </summary>
        public bool Type { get; set; }
        /// <summary>
        /// 标签排序
        /// </summary>
        public int TagOrder { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 是否删除 false 不删除 true 删除
        /// </summary>
        [DefaultValue(false)]
        public bool IsDelete { get; set; }
    }
}
