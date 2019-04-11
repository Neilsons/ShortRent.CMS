using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Core.Config
{
    /// <summary>
    /// 所有的配置
    /// ConfigurationSection是系统的配置节点
    /// </summary>
    public class ApplicationConfig:ConfigurationSection
    {
        #region  Field 字段
        private const string RedisCacheConfigChildName = "redisCache";
        private const string CacheTimeConfigChildNameCollect = "CacheTime";
        #endregion
        #region Property 属性
        /// <summary>
        /// redis配置的解析
        /// </summary>
        [ConfigurationProperty(RedisCacheConfigChildName,IsRequired =true)]
        public RedisCacheElement RedisCacheConfig
        {
            get { return (RedisCacheElement)base[RedisCacheConfigChildName]; }
            set { base[RedisCacheConfigChildName] = value; }
        }
        [ConfigurationProperty(CacheTimeConfigChildNameCollect,IsRequired =true)]
        public CacheTimesElement CacheTimeCollect
        {
            get { return (CacheTimesElement)base[CacheTimeConfigChildNameCollect]; }
            set { base[CacheTimeConfigChildNameCollect]=value; }
        }
        #endregion
    }
}
