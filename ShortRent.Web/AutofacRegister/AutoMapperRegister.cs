using Autofac;
using AutoMapper;
using ShortRent.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web
{
    public class AutoMapperRegister : IDependencyRegister
    {
        public void RegisterTypes(ContainerBuilder container)
        {
            //找到所有继承的Profile
            var profileTypes = this.GetType().Assembly.GetTypes().Where(t=>typeof(Profile).IsAssignableFrom(t));
            //找到所有的实例
            var profileInstances = profileTypes.Select(t=>(Profile)Activator.CreateInstance(t));
            var config = new MapperConfiguration((cfg)=> { profileInstances.ToList().ForEach(t=>cfg.AddProfile(t)); });

            //注册一个单例  使用构造函数注入
            container.RegisterInstance<MapperConfiguration>(config).SingleInstance();
            //以后都可以使用 
            container.RegisterInstance<IMapper>(config.CreateMapper()).SingleInstance();



        }
    }
}