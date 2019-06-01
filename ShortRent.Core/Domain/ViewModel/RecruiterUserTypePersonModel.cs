using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Core.Domain
{
   public class RecruiterUserTypePersonModel
    {
        public int CompanyId { get; set; }

        public int ID { get; set; }
        /// <summary>
        /// 0代表普通用户，1代表招聘
        /// </summary>
        public bool Type { get; set; }
        /// <summary>
        /// 用户类型ID
        /// </summary>
        public int UserTypeId { get; set; }
        /// <summary>
        /// 信用积分
        /// </summary>
        public decimal CreditScore { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
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
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        public int BusinessTypeId { get; set; }        

    }
}
