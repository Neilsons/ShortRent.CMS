using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Core.Domain
{
    public class PublishMsg : BaseEntity
    {
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 币种 这里是符号
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// 开始区间
        /// </summary>
        public int StartSection { get; set; }
        /// <summary>
        /// 结束区间
        /// </summary>
        public int EndSection { get; set; }
        /// <summary>
        /// 内容简介
        /// </summary>
        public string Decription { get; set; }
        /// <summary>
        /// 详情信息
        /// </summary>
        public string Detail { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 是否删除 false不删除 true 删除
        /// </summary>
        [DefaultValue(false)]
        public bool IsDelete { get; set; }

        /// <summary>
        /// 用户类别的外键
        /// </summary>
        public int UserTypeInfoId { get; set; }
        /// <summary>
        /// 行业id
        /// </summary>
        public int BusinessTypeId { get; set; }

        public virtual Business Business{get;set;}

        public virtual UserType UserType { get; set;}
        /// <summary>
        /// 所有的评论
        /// </summary>
        public virtual ICollection<Discuss> Discusss { get; set; }
        public virtual ICollection<CompanyPerTag> CompanyPerTags { get; set; }

    }
}
