using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Areas.ShortWeb.Models
{
    public class WebRegister
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
        /// 是否招聘 false 招聘 true 被招聘
        /// </summary>
        public bool IsRecruit { get; set; }
        /// <summary>
        /// 身份证前面
        /// </summary>
        public string IdCardFront { get; set; }
        /// <summary>
        /// 身份证反面
        /// </summary>
        public string IdCardBack { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 公司积分
        /// </summary>
        public decimal Score { get; set; }
        /// <summary>
        /// 公司logo
        /// </summary>
        public string CompanyImg { get; set; }
        /// <summary>
        /// 介绍信息
        /// </summary>
        public string Introduction { get; set; }
        /// <summary>
        /// 公司营业执照
        /// </summary>
        public string  CompanyLicense {get;set;}
        /// <summary>
        /// 员工数量
        /// </summary>
        public int EmployeesCount { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime EstablishTime { get; set; }
        /// <summary>
        /// 地址信息
        /// </summary>
        public string Address { get; set; }

        public string Confirm { get; set; }
    }
}