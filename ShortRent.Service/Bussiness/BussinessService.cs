using ShortRent.Core;
using ShortRent.Core.Cache;
using ShortRent.Core.Config;
using ShortRent.Core.Data;
using ShortRent.Core.Domain;
using ShortRent.Core.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Service
{
    public class BussinessService:BaseService,IBussinessService
    {
        #region Fields
        private readonly IRepository<Business> _bussinessRepository;
        private readonly ICacheManager _cacheManager;
        private readonly ILogger _logger;
        private readonly ApplicationConfig _config;
        private const string BussinessCacheKey = nameof(BussinessService) + nameof(Business);
        #endregion
        #region Constroctor
        public BussinessService(IRepository<Business> bussinessRepository, ICacheManager cacheManager, ILogger logger, ApplicationConfig config)
        {
            _bussinessRepository = bussinessRepository;
            _cacheManager = cacheManager;
            _logger = logger;
            _config = config;
        }
        #endregion
        #region  Methods
        public void CreateBussiness(Business model)
        {
            _bussinessRepository.Insert(model);
            _cacheManager.Remove(BussinessCacheKey);
        }
        public List<Business> GetPagedBussiness(int pageSize, int pageNumber, out int total)
        {
            List<Business> models = null;
            try
            {
                if (_cacheManager.Contains(BussinessCacheKey))
                {
                    var cache = _cacheManager.Get<List<Business>>(BussinessCacheKey);
                    models = cache.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                    total = cache.Count();
                }
                else
                {
                    var list = _bussinessRepository.Entitys;
                    list = list.OrderByDescending(c => c.CreateTime).ToList();
                    if (list.Any())
                    {
                        int cacheTime = GetTimeFromConfig((int)CacheTimeLev.lev1);
                        models = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                        total = list.Count();
                        _cacheManager.Set(BussinessCacheKey, list, TimeSpan.FromMinutes(cacheTime));
                    }
                    else
                    {
                        models = new List<Business>();
                        total = 0;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Debug("返回行业分页信息出错", e);
                throw e;
            }
            return models;
        }
        public Business GetBussiness(int id)
        {
            Business model = null;
            try
            {
                if (_cacheManager.Contains(BussinessCacheKey))
                {
                    model = _cacheManager.Get<List<Business>>(BussinessCacheKey).Find(c => c.ID == id);
                }
                else
                {
                    var list = _bussinessRepository.Entitys;
                    if (list.Any())
                    {
                        model = list.Where(c => c.ID == id).FirstOrDefault();
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error("获取一个行业列表失败！", e);
                throw e;
            }
            if (model == null)
            {
                model = new Business();
            }
            return model;
        }
        public void UpdateBussiness(Business model)
        {
            _bussinessRepository.Update(model);
            _cacheManager.Remove(BussinessCacheKey);
        }
        public List<Business> GetBussinesss()
        {
            List<Business> models = null;
            try
            {
                if (_cacheManager.Contains(BussinessCacheKey))
                {
                    models = _cacheManager.Get<List<Business>>(BussinessCacheKey);
                }
                else
                {
                    var list = _bussinessRepository.Entitys;
                    models = list.OrderByDescending(c => c.CreateTime).ToList();
                    if (list.Any())
                    {
                        int cacheTime = GetTimeFromConfig((int)CacheTimeLev.lev1);
                        _cacheManager.Set(BussinessCacheKey, models, TimeSpan.FromMinutes(cacheTime));
                    }
                    else
                    {
                        models = new List<Business>();
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Debug("返回行业信息出错", e);
                throw e;
            }
            return models;
        }
        #endregion
    }
}
