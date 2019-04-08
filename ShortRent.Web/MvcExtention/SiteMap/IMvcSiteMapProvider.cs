using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ShortRent.Web.MvcExtention
{
    public interface IMvcSiteMapProvider
    {
        IEnumerable<MvcSiteMapNode> GetSiteMap(ViewContext context);
        IEnumerable<MvcSiteMapNode> GetBreadCrumb(ViewContext context);
    }
}
