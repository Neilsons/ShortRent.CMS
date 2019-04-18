using ShortRent.Web.Models;
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
        IEnumerable<ManagerBread> GetSiteMap(ViewContext context);
        IEnumerable<ManagerBread> GetBreadCrumb(ViewContext context);
    }
}
