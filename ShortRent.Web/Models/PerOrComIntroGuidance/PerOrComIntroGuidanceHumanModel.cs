using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Models
{
    public class PerOrComIntroGuidanceHumanModel
    {
        //编号
        public int ID { get; set; }
        /// <summary>
        /// 用户问题
        /// </summary>
        public string QuestionMsg { get; set; }
        /// <summary>
        /// 用户问题分类 0代表用户 1代表招聘
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}