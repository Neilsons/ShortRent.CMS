using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Core.Domain
{
    public class Role : BaseEntity
    {
        /// <summary>
        /// 角色名称 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// false 代表前台 true 代表后台
        /// </summary>
        public bool Type { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime{get;set;}
        /// <summary>
        /// 是否删除 false 不删除 true 删除
        /// </summary>
        [DefaultValue(false)]
        public bool IsDelete { get; set; }

    }
}
