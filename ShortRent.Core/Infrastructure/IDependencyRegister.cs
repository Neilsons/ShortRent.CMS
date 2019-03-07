using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace ShortRent.Core.Infrastructure
{
    /// <summary>
    /// 依赖注入的接口
    /// 依赖注入都需要实现这个，可以有能力添加自己的实现，实现业务替换的一个过程
    /// 每一层都可以创建一个
    /// </summary>
    public interface IDependencyRegister
    {
        void RegisterTypes(ContainerBuilder container);
    }
}
