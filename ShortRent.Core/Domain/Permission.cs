using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Core.Domain
{
    public class Permission:BaseEntity
    {
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 权限类别 false 前台 true 后台
        /// </summary>
        public bool Type { get; set; }
        /// <summary>
        /// 权限分类
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
