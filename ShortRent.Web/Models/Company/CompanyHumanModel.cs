﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Models
{
    public class CompanyHumanModel
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 公司信用积分
        /// </summary>
        public decimal Score { get; set; }
        /// <summary>
        /// 公司logo
        /// </summary>
        public string CompanyImg { get; set; }
        /// <summary>
        /// 公司简介
        /// </summary>
        public string Introduction { get; set; }
        /// <summary>
        /// 公司营业执照 pdf文档地址
        /// </summary>
        public string CompanyLicense { get; set; }
        /// <summary>
        /// 员工数量
        /// </summary>
        public int EmployeesCount { get; set; }
        /// <summary>
        /// 公司成立时间
        /// </summary>
        public DateTime EstablishTime { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 创建时间 
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 公司状态 0 未审核，1审核通过，2审核未通过。
        /// </summary>
        public string CompanyStatus { get; set; }
        /// <summary>
        /// 审核的信息
        /// </summary>
        public string CompanyMessage { get; set; }
    }
}