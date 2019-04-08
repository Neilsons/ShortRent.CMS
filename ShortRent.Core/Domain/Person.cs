using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Core.Domain
{
    public class Person : BaseEntity
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// null保密 true 男 false 女
        /// </summary>
        public bool? Sex { get; set; }
        /// <summary>
        /// 用户类别 0代表前台用户，1 代表后台用户
        /// </summary>
        public bool Type { get; set; }
        /// <summary>
        /// 个人头像
        /// </summary>
        public string PerImage { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord{get;set;}
        /// <summary>
        /// 信用积分
        /// </summary>
        [DefaultValue(100)]
        public decimal CreditScore { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard { get; set; }
        /// <summary>
        /// 用户排序
        /// </summary>
        public int PerOrder { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        public string Qq { get; set; }
        /// <summary>
        /// 微信
        /// </summary>
        public string WeChat { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 是否删除 false 不删除 true 删除
        /// </summary>
        [DefaultValue(false)]
        public bool IsDelete { get; set; }

        public virtual ICollection<UserType> UserTypes { get; set; } = new List<UserType>();

        public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
        public virtual ICollection<HistoryOperator> HistoryOperators { get; set; } = new List<HistoryOperator>();
    }
}
