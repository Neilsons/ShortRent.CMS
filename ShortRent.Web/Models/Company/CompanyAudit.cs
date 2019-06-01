using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Models
{
    public class CompanyAudit
    {
        public int ID { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 公司信用积分
        /// </summary>
        public decimal Score { get; set; }
        /// <summary>
        /// 公司营业执照 pdf文档地址
        /// </summary>
        public string CompanyLicense { get; set; }
        /// <summary>
        /// 公司成立时间
        /// </summary>
        public DateTime EstablishTime { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 审核通过，未审核，审核未通过。
        /// </summary>
        public int CompanyStatus { get; set; }
        /// <summary>
        /// 审核的信息
        /// </summary>
        public string CompanyMessage { get; set; }
    }
}