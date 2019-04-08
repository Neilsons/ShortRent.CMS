using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Web.MvcExtention
{
    public class MvcSiteMapNode
    {
        public bool IsMenu { get; set; }
        public string IconClass { get; set; }
        public string Color { get; set; }
        public bool IsActive { get; set; }
        public bool HasActiveChildren { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public MvcSiteMapNode Parent{get;set;}
        public IEnumerable<MvcSiteMapNode> Children { get; set; }
    }
}
