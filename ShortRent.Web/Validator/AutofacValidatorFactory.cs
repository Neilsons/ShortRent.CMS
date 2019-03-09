using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;

namespace ShortRent.Web
{
    /// <summary>
    /// 使用autofac自动注入 验证器工厂 继承自验证器工厂基类
    /// </summary>
    public class AutofacValidatorFactory:ValidatorFactoryBase
    {
        #region Field 
        /// <summary>
        /// 容器工厂
        /// </summary>
        private readonly IContainer _autofacContainer;
        #endregion
        #region Contrustor
        public AutofacValidatorFactory(IContainer containerBuilder)
        {
            _autofacContainer = containerBuilder;
        }
        #endregion
        #region Method
        public override IValidator CreateInstance(Type validatorType)
        {
            IValidator validator = null;
            try
            {
                //解析
                validator = _autofacContainer.ResolveNamed<IValidator>(validatorType.GetGenericArguments().First().FullName);
            }
            catch(Exception)
            {
                throw;
            }
            return validator as IValidator;
        }
        #endregion
    }
}