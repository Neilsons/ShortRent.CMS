using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Core.Domain
{
    public  class Discuss:BaseEntity
    {
        /// <summary>
        /// 评论信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 所属楼别
        /// </summary>
        public int Floor { get; set; }
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
        /// 发布信息ID PublishMsg外键
        /// </summary>
        public int PublishId { get; set; }
        /// <summary>
        /// 评论人Id UserType外键
        /// </summary>
        public int UserTypeInfoId { get; set; }
        /// <summary>
        /// 父级ID 自己
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 父级导航属性
        /// </summary>
        public virtual Discuss Parent { get; set; }
        public virtual ICollection<Discuss> ChildDiscuss { get; set; }
        /// <summary>
        /// 谁评论导航属性
        /// </summary>
        public virtual UserType UserType { get; set; }
        /// <summary>
        ///  发布信息的导航属性
        /// </summary>
        public virtual PublishMsg PublishMsg { get; set; }
    }
}
