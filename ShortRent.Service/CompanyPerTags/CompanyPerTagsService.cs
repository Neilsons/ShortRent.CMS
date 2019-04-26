using ShortRent.Core.Cache;
using ShortRent.Core.Config;
using ShortRent.Core.Data;
using ShortRent.Core.Domain;
using ShortRent.Core.Log;
using ShortRent.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace ShortRent.Service
{
    public  class CompanyPerTagsService:BaseService,ICompanyPerTagsService
    {
        #region Field
        private readonly IRepository<CompanyPerTag> _companyPerTagsRepository;
        private readonly ICacheManager _cacheManager;
        private readonly ILogger _logger;
        private readonly ApplicationConfig _config;
        /// <summary>
        /// 缓存的键
        /// </summary>
        private const string CompanyPerTagsCacheKey = nameof(CompanyPerTagsService) + nameof(CompanyPerTag);
        #endregion

        #region Construction
        public CompanyPerTagsService(IRepository<CompanyPerTag> companyPerTagRepository, ICacheManager cacheManager, ILogger logger, ApplicationConfig config)
        {
            this._companyPerTagsRepository = companyPerTagRepository;
            this._cacheManager = cacheManager;
            this._logger = logger;
            this._config = config;
        }
        #endregion
        #region 
        /// <summary>
        /// 返回是发布消息的标签
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="tagName"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<CompanyPerTag> GetPagedCompanyPerTags(int pageSize,int pageNumber,string tagName,out int total)
        {
            List<CompanyPerTag> models = null;
            try
            {
                Expression<Func<CompanyPerTag, bool>> expression = test => true;
                if (!string.IsNullOrWhiteSpace(tagName))//条件
                {
                    expression = expression.And(c => c.Name.Contains(tagName));
                }
                //得到是发布消息的标签
                expression = expression.And(c => c.IsPublish != null);
                if (_cacheManager.Contains(CompanyPerTagsCacheKey))
                {
                    var cache = _cacheManager.Get<List<CompanyPerTag>>(CompanyPerTagsCacheKey).Where(expression.Compile());
                    models = cache.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                    total = cache.Count();
                }
                else
                {
                    var list = _companyPerTagsRepository.Entitys;
                    list = list.OrderByDescending(c => c.CreateTime).ToList();
                    if (list.Any())
                    {
                        int cacheTime = GetTimeFromConfig((int)CacheTimeLev.lev1);
                        models = list.Where(expression.Compile()).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                        total = list.Where(expression.Compile()).Count();
                        _cacheManager.Set(CompanyPerTagsCacheKey, list, TimeSpan.FromMinutes(cacheTime));
                    }
                    else
                    {
                        models = new List<CompanyPerTag>();
                        total = 0;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Debug("返回标签分页信息出错", e);
                throw e;
            }
            return models;
        }
        public void Create(CompanyPerTag model)
        {
            try
            {
                _companyPerTagsRepository.Insert(model);
                _cacheManager.Remove(CompanyPerTagsCacheKey);
            }
            catch (Exception e)
            {
                _logger.Error("创建标签业务方面出错！",e);
                throw e;
            }
        }
        public CompanyPerTag GetCompanyPerTags(int id)
        {
            CompanyPerTag model = null;
            try
            {
                if (_cacheManager.Contains(CompanyPerTagsCacheKey))
                {
                    model = _cacheManager.Get<List<CompanyPerTag>>(CompanyPerTagsCacheKey).Find(c => c.ID == id);
                }
                else
                {
                    var list = _companyPerTagsRepository.Entitys;
                    if (list.Any())
                    {
                        model = list.Where(c => c.ID == id).FirstOrDefault();
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error("获取一个发布内容标签失败！", e);
                throw e;
            }
            if (model == null)
            {
                model = new CompanyPerTag();
            }
            return model;
        }
        public void Update(CompanyPerTag model)
        {
            try
            {
                _companyPerTagsRepository.Update(model);
                _cacheManager.Remove(CompanyPerTagsCacheKey);
            }
            catch (Exception e)
            {
                _logger.Error("编辑标签业务方面出错！",e);
                throw e;
            }
        }
        public void Delete(CompanyPerTag model)
        {
            try
            {
                _companyPerTagsRepository.Delete(model);
                _cacheManager.Remove(CompanyPerTagsCacheKey);
            }
            catch (Exception e)
            {
                _logger.Error("删除标签业务方面出错！", e);
                throw e;
            }
        }
        #endregion
    }
}
