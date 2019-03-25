using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Core.Domain
{
    public class Collect : BaseEntity
    {
        /// <summary>
        /// 收藏人的id UserType表中的外键
        /// </summary>
        public int UserTypeId{get;set;}
        /// <summary>
        /// 收藏的公司Id 外键
        /// </summary>
        public int CollectCompanyId { get; set; }
        /// <summary>
        /// 收藏的用户的Id UserType中的外键
        /// </summary>
        public int CollectUserId { get; set; }
        /// <summary>
        /// false 代表用户收藏公司 true代表公司收藏 用户
        /// </summary>
        public bool Type { get; set; }
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
