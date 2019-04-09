using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace ShortRent.Web.Models
{
    public class RoleViewModelIndex
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
        [UIHint("RoleType")]
        public bool Type { get; set; }
    }
}