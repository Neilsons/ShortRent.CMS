using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Models
{
    public class PassWordEditModel
    {
        public int ID { get; set; }
        public string OldPassWord{get;set;}
        public string PassWord { get; set; }
        public string ConfirmPassWord { get; set; }
    }
}