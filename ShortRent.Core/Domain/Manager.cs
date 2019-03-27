using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Core.Domain
{
    public class Manager:BaseEntity
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 控制器名称
        /// </summary>
        public string ControllerName { get; set; }
        /// <summary>
        /// 方法名称
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// 是否活动 false 活动 true 不活动
        /// </summary>
        [DefaultValue(false)]
        public bool Activity { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string ClassIcons { get; set; }
        /// <summary>
        /// 图标颜色
        /// </summary>
        public string Color { get; set; }

        public int Pid { get; set; }

        public virtual Manager Parent { get; set; }
        public virtual ICollection<Manager> Childrens { get; set; }
    }
}
