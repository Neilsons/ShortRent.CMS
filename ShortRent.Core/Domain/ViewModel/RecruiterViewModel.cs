using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Core.Domain
{
    public class RecruiterViewModel
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
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 个人头像
        /// </summary>
        public string PerImage { get; set; }
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
    }
}
