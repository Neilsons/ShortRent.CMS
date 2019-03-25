using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Core.Domain
{
    public class Company:BaseEntity
    {
        /// <summary>
        /// 公司名称
        /// </summary>
       public string Name { get; set; }
        /// <summary>
        /// 公司信用积分
        /// </summary>
        [DefaultValue(100)]
       public decimal Score { get; set; }
        /// <summary>
        /// 公司简介
        /// </summary>
       public string Introduction { get; set; }
       /// <summary>
       /// 公司营业执照
       /// </summary>
       public string CompanyImg { get; set; }
       /// <summary>
       /// 创建时间 
       /// </summary>
       public DateTime CreateTime { get; set; }
       /// <summary>
       /// 是否删除 false 不删除 true删除
       /// </summary>
       [DefaultValue(false)]
       public bool IsDelete { get; set; }
    }
}
