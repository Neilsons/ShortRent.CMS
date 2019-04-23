using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Models
{
    public class PersonAdminHumanEditModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string PerImage { get; set; }
        public string PassWord { get; set; }

        public string Position { get; set; }
        public string PersonDetail { get; set; }
        public string Qq { get; set; }
        public string WeChat { get; set; }
        public DateTime CreateTime { get; set; }

    }
}