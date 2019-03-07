using System;
using System.Collections.Generic;
using ShortRent.Core.Infrastructure;
using System.Linq;
using System.Web;
using Autofac;
using ShortRent.WebCore.Infrastructure;
using System.Configuration;
using ShortRent.Core.Config;

namespace ShortRent.Web.App_Start
{
    public static class AutofacConfig
    {
        public static ContainerBuilder GetConfiguredContainer()
        {
            RegisterTypes(ServiceContainer.Current);
            return ServiceContainer.Current;
        }
        public static IContainer GetConfiguratedBulid()
        {
            return ServiceContainer.CurrentBuilder;
        }
        public static void RegisterTypes(ContainerBuilder container)
        {
            //在程序启动的时候将配直节初始化将配置节注入进去
            var config = ConfigurationManager.GetSection("applicationConfig") as ApplicationConfig;
            container.RegisterInstance<ApplicationConfig>(config);
            ITypeFinder typeFinder = new WebTypeFinder();
            //实现这个接口的都要注入
            var registerTypes = typeFinder.FindClassesOfType<IDependencyRegister>();
            foreach(Type registerType in registerTypes)
            {
                var register = (IDependencyRegister)Activator.CreateInstance(registerType);
                register.RegisterTypes(container);
            }

        }
    }
}