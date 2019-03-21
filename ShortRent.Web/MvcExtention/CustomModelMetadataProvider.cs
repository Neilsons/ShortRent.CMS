using ShortRent.Web.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace ShortRent.Web
{
    /// <summary>
    /// 自定义元数据提供者解决显示的内容
    /// </summary>
    public class CustomModelMetadataProvider:DataAnnotationsModelMetadataProvider
    {
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            //将mvc默认的先存起来
            var modelMetadata = base.CreateMetadata(attributes,containerType,modelAccessor,modelType,propertyName);
            if(containerType!=null)
            {
                string key = containerType.Name.Replace(".",string.Empty)+propertyName+nameof(modelMetadata.DisplayName);
                //从资源中拿根据键
                string displayName=Resources.ResourceManager.GetString(key);
                if(!string.IsNullOrWhiteSpace(displayName))
                {
                    modelMetadata.DisplayName = displayName;
                }
            }
            return modelMetadata;
        }
    }
}