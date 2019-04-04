using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Core.Domain
{
    public class UserType:BaseEntity
    {
        /// <summary>
        /// 0代表普通用户，1代表招聘
        /// </summary>
        public bool Type { get; set; }
        /// <summary>
        /// 身份证正面
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
        /// 公司证件照或营业执照
        /// </summary>
        public string CompanyImg { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 是否删除 false不删除 true删除
        /// </summary>
        [DefaultValue(false)]
        public bool IsDelete { get; set; }
        
        /// <summary>
        /// Person中的外键
        /// </summary>
        public int PersonId { get; set; }

        public virtual Person Person { get; set; }
        /// <summary>
        /// 发布信息列表
        /// </summary>
        public virtual ICollection<PublishMsg> PublishMsgs { get; set; }
        /// <summary>
        /// 问题域集合
        /// </summary>
        public virtual ICollection<PerOrComIntro> PerOrComIntros { get; set; }

    }
}
