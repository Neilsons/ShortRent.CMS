using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Models
{
    public class IconInfoShowModel
    {
        //编号
        public int ID { get; set; }
        //前缀
        public string Prefix { get; set; }
        //内容
        public string Content { get; set; }
        //准确图标展示
        public string IconInfo { get; set; }
    }
}