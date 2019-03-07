using ShortRent.Core.Config;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Core.Cache
{
    /// <summary>
    /// 分布式缓存的实现
    /// </summary>
    public class RedisCacheManager : ICacheManager
    {
        #region Field
        private readonly string redisConnectionString;
        /// <summary>
        /// 保持一个长连接的redis需要加锁
        /// </summary>
        private volatile ConnectionMultiplexer redisConnection;
        private readonly object redisConnectionLockne = new object();
        #endregion

        #region Construction
        public RedisCacheManager(ApplicationConfig config)
        {
            if (string.IsNullOrWhiteSpace(config.RedisCacheConfig.ConnectionString))
            {
                throw new ArgumentException("redis config is empty", nameof(config));
                this.redisConnectionString = config.RedisCacheConfig.ConnectionString;
                this.redisConnection = GetRedisConnection();
            }
        }
        #endregion
        
        #region private Method
        private ConnectionMultiplexer GetRedisConnection()
        {
           if(this.redisConnection!=null&&redisConnection.IsConnected)
            {
                return redisConnection;
            }
            lock (redisConnectionLockne)//多线程加锁
            {
                if (this.redisConnection != null)//没有连接就释放
                {
                    redisConnection.Dispose();
                }
                this.redisConnection = ConnectionMultiplexer.Connect(redisConnectionString);
            }
            return this.redisConnection;
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="value">字节数组</param>
        /// <returns></returns>
        private T Deserialize<T>(byte[] value)
        {
            if(value==null)
            {
                return default(T);
            }
            var jsonString = Encoding.UTF8.GetString(value);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonString);
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="item">实体</param>
        /// <returns></returns>
        private byte[] Serialize(object item)
        {
            var jsonString=Newtonsoft.Json.JsonConvert.SerializeObject(item);
            return Encoding.UTF8.GetBytes(jsonString);
        }
        #endregion

        #region  public Method
        public void Clear()
        {
            foreach(var endPoint in this.GetRedisConnection().GetEndPoints())
            {
                var server = this.GetRedisConnection().GetServer(endPoint);
                foreach(var key in server.Keys())
                {
                    this.Remove(key);
                }
            }
        }

        public bool Contains(string key)
        {
            return redisConnection.GetDatabase().KeyExists(key);
        }

        public T Get<T>(string key)
        {
            var value = redisConnection.GetDatabase().StringGet(key);
            if(value.HasValue)
            {
                return Deserialize<T>(value);
            }
            else
            {
                return default(T);
            }
        }
        public void Remove(string key)
        {
            redisConnection.GetDatabase().KeyDelete(key);
        }

        public void Set(string key, object value, TimeSpan cacheTime)
        {
           if(value!=null)
            {
                redisConnection.GetDatabase().StringSet(key,Serialize(value),cacheTime);
            }
        }
        #endregion
    }
}
