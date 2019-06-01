﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Areas.ShortWeb.Models
{
    public class ContactViewModel
    {
        public int ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Brief { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}