using ShortRent.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShortRent.Core.Infrastructure;

namespace ShortRent.Service
{
    public class BaseService
    {
        #region Fields
        private readonly ApplicationConfig _config;
        #endregion
        #region Construction
        public BaseService()
        {
            _config = ServiceContainer.Resolve<ApplicationConfig>();
        }
        #endregion
        #region Commonality Methods
        /// <summary>
        /// 从配置文件中获取缓存时间
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected int GetTimeFromConfig(int type)
        {
            int time = 0;
            if(_config.CacheTimeCollect.Enable)
            {
                if (type == 1)
                {
                    time = _config.CacheTimeCollect.Level1.timeMinutes;
                }
                else if (time == 2)
                {
                    time = _config.CacheTimeCollect.Level2.timeMinutes;
                }
            }
            return time;
        }
        #endregion
    }
}
