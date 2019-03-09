using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using FluentValidation;
using ShortRent.Core.Infrastructure;

namespace ShortRent.Web.Validator
{
    /// <summary>
    /// 验证器的注册
    /// </summary>
    public class ValidatorRegister : IDependencyRegister
    {
        public void RegisterTypes(ContainerBuilder container)
        {
            var validatorTypes = this.GetType().Assembly.GetTypes().Where(t => t.GetInterfaces().Any(i => i.IsGenericType&&i.GetGenericTypeDefinition() == typeof(IValidator<>)));
            foreach(Type type in validatorTypes)
            {
                //这个地方因为有好多都从验证注册为Ivalidator，所以提供一个名字来区分使用
                container.RegisterType(type).As(typeof(IValidator)).Named<IValidator>(type.BaseType.GetGenericArguments().First().FullName);
            }
        }
    }
}