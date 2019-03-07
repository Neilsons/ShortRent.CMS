using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//一个允许其他包在Web应用程序中执行某些启动代码的包。
using WebActivatorEx;
using Autofac;
using Autofac.Integration.Mvc;
using ShortRent.Web.Controllers;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ShortRent.Web.App_Start.AutofacWebActivator), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(ShortRent.Web.App_Start.AutofacWebActivator), "Shutdown")]

namespace ShortRent.Web.App_Start
{
    public static class AutofacWebActivator
    {
        public static void Start()
        {
            var container = AutofacConfig.GetConfiguredContainer();
            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            container.RegisterFilterProvider();
            container.RegisterControllers(typeof(MvcApplication).Assembly);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(AutofacConfig.GetConfiguratedBulid()));
        }
        public  static void Shutdown()
        {
            var containerBulid = AutofacConfig.GetConfiguratedBulid();
            containerBulid.Dispose();
        }
    }
}