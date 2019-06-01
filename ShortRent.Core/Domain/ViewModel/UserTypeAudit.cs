using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Core.Domain
{
    public class UserTypeAudit
    {
        public int ID { get; set; }
        /// <summary>
        /// 用户类型ID
        /// </summary>
        public int UserTypeId { get; set; }
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
        /// 信用积分
        /// </summary>
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
        /// 身份证正面
        /// </summary>
        public string IdCardFront { get; set; }
        /// <summary>
        /// 身份证反面
        /// </summary>
        public string IdCardBack { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建者的用户类型0未审核，1审核通过，2审核不通过
        /// </summary>
        public int TypeUser { get; set; }
        /// <summary>
        /// 用户返回的信息
        /// </summary>
        public string TypeMessage { get; set; }

    }
}