using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Core.Domain
{
    public class EntityPermission:BaseEntity
    {
        /// <summary>
        /// 实体名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 创建时间 
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 是否删除 false 不删除 true 删除
        /// </summary>
        [DefaultValue(false)]
        public bool IsDelete { get; set; }
        /// <summary>
        /// 角色 Id Role 外键
        /// </summary>
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
