using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using FluentValidation;
using ShortRent.Core.Infrastructure;
using ShortRent.Resource;
using ShortRent.Resource.MetaData;

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
            //设置资源
            FluentValidation.Mvc.FluentValidationModelValidatorProvider.Configure();
            //这个时显示的名字
            ValidatorOptions.DisplayNameResolver = (type, memberInfo, lambdaExression) =>
            {
                string key = type.Name + memberInfo.Name + "DisplayName";
                //从资源中拿根据键
                string displayName = ResourceManagers.getViewElement(key);
                return displayName;
            };
            //资源从资源文件中获取
            ValidatorOptions.ResourceProviderType = typeof(Resources);          
            foreach(Type type in validatorTypes)
            {
                //这个地方因为有好多都从验证注册为Ivalidator，所以提供一个名字来区分使用
                container.RegisterType(type).As(typeof(IValidator)).Named<IValidator>(type.BaseType.GetGenericArguments().First().FullName);
            }
        }
    }
}