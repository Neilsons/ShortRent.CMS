﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Core.Cache
{
    /// <summary>
    /// 内存缓存的实现
    /// </summary>
    public class MemoryCacheManager : ICacheManager
    {
        #region  Method
        public void Clear()
        {
           foreach(var item in MemoryCache.Default)
            {
                this.Remove(item.Key);
            }
        }

        public bool Contains(string key)
        {
            return MemoryCache.Default.Contains(key);
        }

        public T Get<T>(string key)
        {
           return  (T)MemoryCache.Default.Get(key);
        }

        public void Remove(string key)
        {
            MemoryCache.Default.Remove(key);
        }

        public void Set(string key, object value, TimeSpan cacheTime)
        {
            MemoryCache.Default.Add(key,value,new CacheItemPolicy { SlidingExpiration=cacheTime});
        }
        #endregion
    }
}
