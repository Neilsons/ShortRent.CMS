using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Models
{
    public class RoleHumanModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 角色名称 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// false 代表前台 true 代表后台
        /// </summary>
        public string Type { get; set; }
    }
}