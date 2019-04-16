using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShortRent.Core.Cache;
using ShortRent.Core.Data;
using ShortRent.Core.Domain;
using ShortRent.Core.Log;
using ShortRent.Core;

namespace ShortRent.Service
{
    class LogInfoService :BaseService,ILogInfoService
    {
        #region Fields
        //日志信息不加入缓存
        private readonly IRepository<LogInfo> _logInfoReopsitory;
        private readonly ILogger _logger;
        private readonly ICacheManager _cacheManager;
        private const string LoginfoCacheKey= nameof(LogInfoService) + nameof(LogInfo);
        #endregion
        #region Construction
        public LogInfoService(IRepository<LogInfo> repository,ILogger logger,ICacheManager cachemanager)
        {
            this._logInfoReopsitory = repository;
            this._logger = logger;
            this._cacheManager = cachemanager;
        }
        #endregion
        #region  Methods
        /// <summary>
        /// 删除一个日志
        /// </summary>
        /// <param name="info"></param>
        public void Delete(LogInfo info)
        {
            _logInfoReopsitory.Delete(info);
        }
        /// <summary>
        /// 获得所有的
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<LogInfo> GetLogInfos()
        {
            int total;
            return GetLogPagedListInfo(0,0,out total);
        }
        /// <summary>
        /// 获得分页的
        /// </summary>
        /// <param name="pagedIndex"></param>
        /// <param name="pagedSize"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<LogInfo> GetLogPagedListInfo(int pagedIndex, int pagedSize, out int total)
        {
            List<LogInfo> models = null;
            try
            {
                if (_cacheManager.Contains(LoginfoCacheKey))
                {
                    var list = _cacheManager.Get<List<LogInfo>>(LoginfoCacheKey);
                    if (pagedIndex == 0 && pagedSize == 0)
                        models = list.ToList();
                    else
                        models = list.Skip((pagedIndex - 1) * pagedSize).Take(pagedSize).ToList();
                    total = list.Count();
                }
                else
                {
                    var list = _logInfoReopsitory.Entitys.OrderByDescending(c => c.CreateTime).ToList();
                    if (pagedIndex == 0 && pagedSize == 0)
                    {
                        models = list;
                    }
                    else
                    {
                        models = list.Skip((pagedIndex - 1) * pagedSize).Take(pagedSize).ToList();
                    }
                    total = list.Count();
                    _cacheManager.Set(LoginfoCacheKey, list, TimeSpan.FromMinutes(GetTimeFromConfig((int)CacheTimeLev.lev1)));
                }
            }
            catch (Exception e)
            {
                _logger.Error("获取日志信息出错！", e);
                throw e;
            }
            return models;
        }
        public LogInfo GetDetail(int id)
        {
            LogInfo logInfo = null;
            try
            {
                if(_cacheManager.Contains(LoginfoCacheKey))
                {
                    logInfo = _cacheManager.Get<List<LogInfo>>(LoginfoCacheKey).Find(c=>c.ID==id);
                }
                else
                {
                    logInfo = _logInfoReopsitory.Entitys.Where(c => c.ID == id).FirstOrDefault();
                }
            }
            catch(Exception e)
            {
                _logger.Error("从数据库获得详情出错",e);
                throw e;
            }
            return logInfo;
        }
        #endregion

    }
}
