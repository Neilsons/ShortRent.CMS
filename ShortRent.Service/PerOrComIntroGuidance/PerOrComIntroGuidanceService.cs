using ShortRent.Core.Cache;
using ShortRent.Core.Config;
using ShortRent.Core.Data;
using ShortRent.Core.Domain;
using ShortRent.Core.Log;
using ShortRent.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Service
{
    public class PerOrComIntroGuidanceService:BaseService,IPerOrComIntroGuidanceService
    {
        #region Field
        private readonly IRepository<PerOrComIntroGuidance> _perOrComIntroGuidanceRepository;
        private readonly ICacheManager _cacheManager;
        private readonly ILogger _logger;
        private readonly ApplicationConfig _config;
        /// <summary>
        /// 缓存的键
        /// </summary>
        private const string PerOrComIntroGoidAnceServiceCache = nameof(PerOrComIntroGuidanceService) + nameof(PerOrComIntroGuidance);
        #endregion

        #region Construction
        public PerOrComIntroGuidanceService(IRepository<PerOrComIntroGuidance> perOrComIntroGuidanceRepository, ICacheManager cacheManager, ILogger logger, ApplicationConfig config)
        {
            this._perOrComIntroGuidanceRepository = perOrComIntroGuidanceRepository;
            this._cacheManager = cacheManager;
            this._logger = logger;
            this._config = config;
        }
        #endregion
        #region Methods
        public List<PerOrComIntroGuidance> GetPerOrComIntroGuidances()
        {
            int total;
            return GetPagedPerOrComIntor(0,0,null,out total);
        }
        public List<PerOrComIntroGuidance> GetPagedPerOrComIntor(int pageSize,int pageNumber,bool? Type,out int total)
        {
            List<PerOrComIntroGuidance> guidances = null;
            try
            {
                Expression<Func<PerOrComIntroGuidance, bool>> expression = test => true;
                if (Type!=null)//条件
                {
                    expression = expression.And(c=>c.Type==Type);
                }
                if (_cacheManager.Contains(PerOrComIntroGoidAnceServiceCache))
                {
                    var cache = _cacheManager.Get<List<PerOrComIntroGuidance>>(PerOrComIntroGoidAnceServiceCache).Where(expression.Compile());
                    if (pageSize == 0 && pageNumber == 0)
                    {
                        guidances = cache.ToList();
                    }
                    else
                    {
                        guidances = cache.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                    }
                    total = cache.Count();
                }
                else
                {
                    var list = _perOrComIntroGuidanceRepository.Entitys;
                    list = list.OrderByDescending(c => c.CreateTime).Where(c => c.IsDelete == false).ToList();
                    if (list.Any())
                    {
                        int cacheTime = GetTimeFromConfig((int)CacheTimeLev.lev1);
                        if (pageSize == 0 && pageNumber == 0)
                        {
                            guidances = list.Where(expression.Compile()).ToList();
                        }
                        else
                        {
                            guidances = list.Where(expression.Compile()).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                        }
                        total = list.Where(expression.Compile()).Count();
                        _cacheManager.Set(PerOrComIntroGoidAnceServiceCache, list, TimeSpan.FromMinutes(cacheTime));
                    }
                    else
                    {
                        guidances = new List<PerOrComIntroGuidance>();
                        total = 0;
                    }
                }
            }
            catch(Exception e)
            {
                _logger.Debug("获得分页的导航介绍信息错误",e);
                throw e;
            }
            return guidances;
        }
        public void Insert(PerOrComIntroGuidance model)
        {
            try
            {
                _perOrComIntroGuidanceRepository.Insert(model);
                _cacheManager.Remove(PerOrComIntroGoidAnceServiceCache);
            }
            catch (Exception e)
            {
                _logger.Debug("创建问题介绍信息出错！", e);
                throw e;
            }
        }
        public void Update(PerOrComIntroGuidance model)
        {
            try
            {
                _perOrComIntroGuidanceRepository.Update(model);
                _cacheManager.Remove(PerOrComIntroGoidAnceServiceCache);
            }
            catch (Exception e)
            {
                _logger.Debug("编辑问题介绍信息出错！", e);
                throw e;
            }
        }
        public PerOrComIntroGuidance GetIntro(int id)
        {
            PerOrComIntroGuidance model = null;
            try
            {
                if (_cacheManager.Contains(PerOrComIntroGoidAnceServiceCache))
                {
                    model = _cacheManager.Get<List<PerOrComIntroGuidance>>(PerOrComIntroGoidAnceServiceCache).Where(c => c.ID == id).FirstOrDefault();
                }
                else
                {
                    model = _perOrComIntroGuidanceRepository.Entitys.Where(c => c.ID == id && c.IsDelete == false).FirstOrDefault();
                    if (model == null)
                    {
                        model = new PerOrComIntroGuidance();
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Debug("返回一个问题介绍出错", e);
                throw e;
            }
            return model;
        }
        #endregion
    }
}
