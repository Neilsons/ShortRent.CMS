using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Core.Domain
{
    public  class PerOrComIntroGuidance:BaseEntity
    {
        /// <summary>
        /// 用户问题
        /// </summary>
        public string QuestionMsg { get; set; }
        /// <summary>
        /// 用户问题分类 0代表用户 1代表招聘
        /// </summary>
        public bool Type { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 是否删除 false 不删除 true 删除
        /// </summary>
        [DefaultValue(false)]
        public bool IsDelete { get; set; }
    }
}
