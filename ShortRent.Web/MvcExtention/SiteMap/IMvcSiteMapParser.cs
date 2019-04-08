using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShortRent.Web.MvcExtention
{
    public interface IMvcSiteMapParser
    {
        IEnumerable<MvcSiteMapNode> GetNodeTree(XElement sitemap);
    }
}
