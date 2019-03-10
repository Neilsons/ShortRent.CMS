using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FluentValidation;
//这个方法比Global里面的Application_Start运行的早
[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ShortRent.Web.App_Start.ExensionActivator),"Start")]
namespace ShortRent.Web.App_Start
{
    /// <summary>
    /// 扩展激活器
    /// </summary>
    public static class ExensionActivator
    {
        public static void Start()
        {
            //移除掉mvc自己的验证
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            //实例化一个验证工厂
            AutofacValidatorFactory autofacValidator = new AutofacValidatorFactory(AutofacConfig.GetConfiguratedBulid());
            //将验证器换成第三方的
            ModelValidatorProviders.Providers.Add(new FluentValidation.Mvc.FluentValidationModelValidatorProvider(autofacValidator)) ;
            //更改mvc默认的元数据提供者
            ModelMetadataProviders.Current = new CustomModelMetadataProvider();
        }
    }
}