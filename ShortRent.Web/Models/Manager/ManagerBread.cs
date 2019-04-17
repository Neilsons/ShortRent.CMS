using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Models
{
    public class ManagerBread
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
        public ManagerBread Parent { get; set; }
        public ICollection<ManagerBread> Childrens { get; set; }
    }
}