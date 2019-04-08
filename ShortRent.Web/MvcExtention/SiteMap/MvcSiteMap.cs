using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Web.MvcExtention
{
    public class MvcSiteMap
    {
        public IEnumerable<IMvcSiteMapProvider> Provider { get; set; }
    }
}
