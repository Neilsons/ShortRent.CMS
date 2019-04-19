using Autofac;
using ShortRent.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Service
{
    /// <summary>
    /// 服务(业务)的注册
    /// </summary>
    public class ServiceRegister : IDependencyRegister
    {
        public void RegisterTypes(ContainerBuilder container)
        {
            //将PersonService注入
            container.RegisterType<PersonService>().As<IPersonService>();
            container.RegisterType<RoleService>().As<IRoleService>();
            container.RegisterType<PermissionService>().As<IPermissionService>();
            container.RegisterType<HistoryOperatorService>().As<IHistoryOperatorService>();
            container.RegisterType<LogInfoService>().As<ILogInfoService>();
            container.RegisterType<ManagerService>().As<IManagerService>();
            container.RegisterType<IconsInfoService>().As<IIconsInfoService>();
        }
    }
}
