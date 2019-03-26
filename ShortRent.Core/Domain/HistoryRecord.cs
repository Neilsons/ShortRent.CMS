using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Core.Domain
{
    public  class HistoryRecord:BaseEntity
    {
        /// <summary>
        /// false 代表普通用户 true 发表招聘
        /// </summary>
        public bool Type { get; set; }
        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 是否删除 false 不删除 true 删除
        /// </summary>
        [DefaultValue(false)]
        public bool IsDelete { get; set; }
        /// <summary>
        /// UserTypeId 谁访问的记录 UserType 中的外键
        /// </summary>
        public int UserTypeId { get; set; }
        /// <summary>
        /// 发布的信息的Id  publishMsg的外键
        /// </summary>
       
        public int PublishId { get; set; }
    }
}
