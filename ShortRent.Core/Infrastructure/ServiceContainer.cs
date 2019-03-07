using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Core.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceContainer
    {
        #region Field
        /// <summary>
        /// 延迟加载 在不用的时候不去实现
        /// </summary>
        static Lazy<ContainerBuilder> Container = new Lazy<ContainerBuilder>(()=>new ContainerBuilder());
        /// <summary>
        /// 保留着被创建对象的Build
        /// </summary>
        private static Lazy<IContainer> Builder =new Lazy<IContainer>(()=> Container.Value.Build());
        #endregion
        #region Construction
        /// <summary>
        /// 当前的容器初始化值
        /// </summary>
        public static ContainerBuilder Current { get { return Container.Value; } }
        /// <summary>
        /// 存储build
        /// </summary>
        public static IContainer CurrentBuilder { get{ return Builder.Value; } }
        #endregion
        #region Method
        /// <summary>
        /// 通过接口解析出来一个具体的实现
        /// </summary>
        /// <typeparam name="T">具体的实例</typeparam>
        /// <returns></returns>
        public static T Resolve<T>() where T:class
        {
            return CurrentBuilder.Resolve<T>();
        }
        /// <summary>
        /// 当多接口的时候 但是此时无用s
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ResolveAll<T>() where T:class
        {
            return CurrentBuilder.Resolve<IEnumerable<T>>();
        }
        #endregion
    }
}
