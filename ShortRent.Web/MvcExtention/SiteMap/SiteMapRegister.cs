using Autofac;
using ShortRent.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShortRent.Web.MvcExtention;

namespace ShortRent.Web
{
    public class SiteMapRegister : IDependencyRegister
    {
        public void RegisterTypes(ContainerBuilder container)
        {
            container.RegisterType<MvcSiteMapProvider>().As<IMvcSiteMapProvider>();
        }
    }
}