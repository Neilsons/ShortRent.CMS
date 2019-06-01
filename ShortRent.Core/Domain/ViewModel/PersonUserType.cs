using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Core.Domain
{
    public class PersonUserType
    {
        public int ID { get; set; }
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
        public string PassWord { get; set; }
        /// <summary>
        /// 信用积分
        /// </summary>
        public decimal CreditScore { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        public string Position { get; set; }
        public string PersonDetail { get; set; }
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
        public bool IsDelete { get; set; }
        /// <summary>
        /// 创建者的用户类型0未审核，1审核通过，2审核不通过
        /// </summary>
        public int TypeUser { get; set; }
        /// <summary>
        /// 用户返回的信息
        /// </summary>
        public string TypeMessage { get; set; }
        public int UserTypeId { get; set; }
        /// <summary>
        /// 0代表普通用户，1代表招聘
        /// </summary>
        public bool IsReduit { get; set; }
    }
}