using Autofac;
using ShortRent.Core.Data;
using ShortRent.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Data
{
    /// <summary>
    /// 注册仓储
    /// </summary>
    public class RepositoryRegister : IDependencyRegister
    {
        public void RegisterTypes(ContainerBuilder container)
        {
            //注册组件并且暴漏他们的服务(接口)这样对象就可以很好的连接起来了
            container.RegisterType<SRentDbContext>().As<IDbContext>();
            //注册泛型
            container.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerDependency();
        }
    }
}
