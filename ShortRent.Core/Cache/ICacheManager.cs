using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Core.Cache
{
    /// <summary>
    /// 内存缓存，MemoryCache缓存，分布式缓存
    /// </summary>
   public  interface ICacheManager
    {
        /// <summary>
        /// 从缓存中拿一个值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        T Get<T>(string key);
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="cacheTime">缓存时间</param>
        void Set(string key, object value, TimeSpan cacheTime);
        /// <summary>
        /// 缓存中是否包含某个键
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        bool Contains(string key);
        /// <summary>
        /// 移出指定缓存
        /// </summary>
        /// <param name="key">键</param>
        void Remove(string key);
        /// <summary>
        /// 清空缓存
        /// </summary>
        void Clear();

    }
}
