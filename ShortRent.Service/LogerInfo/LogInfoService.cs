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
using System.Linq.Expressions;

namespace ShortRent.Service
{
    class LogInfoService :BaseService,ILogInfoService
    {
        #region Fields
        //日志信息不加入缓存
        private readonly IRepository<LogInfo> _logInfoReopsitory;
        private readonly ILogger _logger;
        #endregion
        #region Construction
        public LogInfoService(IRepository<LogInfo> repository,ILogger logger)
        {
            this._logInfoReopsitory = repository;
            this._logger = logger;
        }
        #endregion
        #region  Methods
        /// <summary>
        /// 获得所有的
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<LogInfo> GetLogInfos()
        {
            int total;
            return GetLogPagedListInfo(0,0,null,null,null,null,out total);
        }
        /// <summary>
        /// 获得分页的
        /// </summary>
        /// <param name="pagedIndex"></param>
        /// <param name="pagedSize"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<LogInfo> GetLogPagedListInfo(int pagedIndex, int pagedSize, string machineName, string catalog, DateTime? startTime, DateTime? endTime, out int total)
        {
            List<LogInfo> models = null;
            try
            {
                Expression<Func<LogInfo, bool>> expression = logInfo => true;
                if(!string.IsNullOrWhiteSpace(machineName))
                {
                    expression = expression.And(c=>c.MachineName.Contains(machineName));
                }
                if(!string.IsNullOrWhiteSpace(catalog)&&catalog!="0")
                {
                    expression = expression.And(c=>c.Catalogue==catalog);
                }
                if(startTime!=null)
                {
                    expression = expression.And(c=>c.CreateTime>=startTime);
                }
                if(endTime!=null)
                {
                    expression = expression.And(c=>c.CreateTime<=endTime);
                }
                var list = _logInfoReopsitory.Entitys.OrderByDescending(c => c.CreateTime).ToList();
                if (pagedIndex == 0 && pagedSize == 0)
                {
                    models = list.Where(expression.Compile()).ToList();
                }
                else
                {
                    models = list.Where(expression.Compile()).Skip((pagedIndex - 1) * pagedSize).Take(pagedSize).ToList();
                }
                total = list.Count();
                   
            }
            catch (Exception e)
            {
                _logger.Error("获取日志信息出错！", e);
                throw e;
            }
            return models;
        }
        /// <summary>
        /// 得到日志的详情信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LogInfo GetDetail(int id)
        {
            LogInfo logInfo = null;
            try
            {
                    logInfo = _logInfoReopsitory.Entitys.Where(c => c.ID == id).FirstOrDefault();
            }
            catch(Exception e)
            {
                _logger.Error("从数据库获得详情出错",e);
                throw e;
            }
            return logInfo;
        }
        /// <summary>
        /// 删除某一个日志
        /// </summary>
        /// <param name="id"></param>
        public LogInfo DeleteById(int id)
        {
            LogInfo logInfo = null;
            try
            {
                //得到要删除的那个实体
                logInfo = GetDetail(id);
                //删除该项
                _logInfoReopsitory.Delete(logInfo);
            }
            catch(Exception e)
            {
                _logger.Error("从数据库删除出错",e);
                throw e;
            }
            return logInfo;
        }
        #endregion

    }
}
