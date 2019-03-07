using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Core.Config
{
    public class RedisCacheElement:ConfigurationElement
    {
        #region  Field
        private const string EnablePropertyName = "enable";
        private const string ConnectionPropertyName = "connectionString";
        #endregion
        #region  Property
        /// <summary>
        /// 允许Redis配置解析
        /// </summary>
        [ConfigurationProperty(EnablePropertyName,IsRequired =true)]
        public bool Enable
        {
            get { return (bool)base[EnablePropertyName]; }
            set { base[EnablePropertyName]=value; }
        }
        /// <summary>
        /// 连接字符串的配置解析
        /// </summary>
        [ConfigurationProperty(ConnectionPropertyName,IsRequired =true)]
        public string ConnectionString
        {
            get { return (string)base[ConnectionPropertyName]; }
            set { base[ConnectionPropertyName] = value; }
        }
        #endregion
    }
}
