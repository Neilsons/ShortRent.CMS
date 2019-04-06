﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Core.Domain
{
    public class PerOrComIntro : BaseEntity
    {
        /// <summary>
        /// 回答的内容
        /// </summary>
        public string Answer { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 是否删除 false 不删除 true删除
        /// </summary>
        [DefaultValue(false)]
        public bool IsDelete { get; set; }
        /// <summary>
        /// 外键问题的编号
        /// </summary>
        public int QuestionId { get; set; }
        /// <summary>
        /// 外键 用户类别编号
        /// </summary>
        public int  UserTypeInfoId{get;set;}
        /// <summary>
        /// 用户或公司介绍表 导航属性
        /// </summary>
        public virtual PerOrComIntroGuidance PerOrComIntroGuidance { get; set; }
        /// <summary>
        /// 用户 导航属性
        /// </summary>
        public virtual UserType UserType { get; set; }
    }
}
