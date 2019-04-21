using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShortRent.Data;
using ShortRent.Core.Data;
using ShortRent.Core.Domain;
using System.Threading.Tasks;
using ShortRent.Core.Cache;
using ShortRent.Core.Config;
using ShortRent.Core.Log;
using System.Linq.Expressions;
using ShortRent.Core;

namespace ShortRent.Service
{
    public class HistoryOperatorService:BaseService,IHistoryOperatorService
    {
        #region Fields
        private readonly IRepository<HistoryOperator> _historyOperatorRepository;
        private readonly ICacheManager _cacheManager;
        private readonly ILogger _logger;
        private readonly ApplicationConfig _config;
        private const string historyOperatorServiceCache = nameof(HistoryOperatorService) + nameof(HistoryOperator);
        #endregion

        #region Construction
        public HistoryOperatorService(IRepository<HistoryOperator> repository, ICacheManager cacheManager, ILogger logger, ApplicationConfig config)
        {
            this._historyOperatorRepository = repository;
            this._cacheManager = cacheManager;
            this._logger = logger;
            this._config = config;

        }
        #endregion

        #region Methods
        /// <summary>
        /// 创建历史记录
        /// </summary>
        /// <param name="model"></param>
        public void CreateHistoryOperator(HistoryOperator model)
        {
            try
            {
                _historyOperatorRepository.Insert(model);
                _cacheManager.Remove(historyOperatorServiceCache);
            }
            catch(Exception e)
            {
                _logger.Debug("创建历史记录",e);
                throw e;
            }
        }
        public void DeleteHistory(HistoryOperator model)
        {
            try
            {
                //删除该项
                _historyOperatorRepository.Delete(model);
                //要将缓存清除
                _cacheManager.Remove(historyOperatorServiceCache);
            }
            catch (Exception e)
            {
                _logger.Error("从数据库删除历史操作出错", e);
                throw e;
            }
        }
        //得到分页历史操作信息
        public List<HistoryOperator> GetHistoryOperators(int pageSize, int pageNumber, string pName, string entityName, out int total)
        {
            List<HistoryOperator> history = null;
            try
            {
                Expression<Func<HistoryOperator, bool>> expression = test => true;
                if (!string.IsNullOrWhiteSpace(pName))//条件
                {
                    expression = expression.And(c=>c.Person.Name.Contains(pName));
                }
                if (!string.IsNullOrWhiteSpace(entityName))//条件
                {
                    expression = expression.And(c => c.EntityModule.Contains(entityName));
                }
                if (_cacheManager.Contains(historyOperatorServiceCache))
                {
                    var model = _cacheManager.Get<List<HistoryOperator>>(historyOperatorServiceCache).Where(expression.Compile());
                    if (pageSize == 0 && pageNumber == 0)
                    {
                        history = model.ToList();
                    }
                    else
                    {
                        history = model.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
                    }
                    total = model.Count();
                }
                else
                {
                    var list = _historyOperatorRepository.IncludeEntitys("Person").ToList();
                    if (list.Any())
                    {
                        if (pageNumber == 0 && pageSize == 0)
                        {
                            history = list.Where(expression.Compile()).ToList();
                        }
                        else
                        {
                            history = list.Where(expression.Compile()).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
                        }
                        total = list.Where(expression.Compile()).Count();
                        int cacheTime = GetTimeFromConfig((int)CacheTimeLev.lev1);
                        _cacheManager.Set(historyOperatorServiceCache, list, TimeSpan.FromMinutes(cacheTime));
                    }
                    else
                    {
                        total = 0;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error("获取操作历史列表出错", e);
                throw e;
            }
            return history;
        }
        public List<HistoryOperator> GetHistoryOperators()
        {
            int total;
            return GetHistoryOperators(0, 0, null,null,out total);
        }
        public HistoryOperator GetHistoryOperator(int id)
        {
            HistoryOperator model = null;
            try
            {
                if (_cacheManager.Contains(historyOperatorServiceCache))
                {
                   
                        model = _cacheManager.Get<List<HistoryOperator>>(historyOperatorServiceCache).Where(c => c.ID == id).FirstOrDefault();
                }
                else
                {
                        model = _historyOperatorRepository.Entitys.Where(c => c.ID == id).FirstOrDefault();
                       
                }
            }
            catch (Exception e)
            {
                _logger.Debug("返回某一个历史操作记录", e);
                throw e;
            }
            if (model == null)
            {
                model = new HistoryOperator();
            }
            return model;
        }
        #endregion
    }
}
