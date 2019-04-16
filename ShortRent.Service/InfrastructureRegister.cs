using Autofac;
using ShortRent.Core.Cache;
using ShortRent.Core.Log;
using ShortRent.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Service
{
    /// <summary>
    /// 基础设施的注入这个是在ShortRent.Core里面的Infrastructure文件夹的
    /// </summary>
    public class InfrastructureRegister : IDependencyRegister
    {
        public void RegisterTypes(ContainerBuilder container)
        {
            //注册缓存
            container.RegisterType<MemoryCacheManager>().As<ICacheManager>();
            //注册日志
            container.RegisterType<NLogLogger>().As<ILogger>();
        }
    }
}
