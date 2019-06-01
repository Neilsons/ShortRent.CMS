using ShortRent.Core;
using ShortRent.Core.Cache;
using ShortRent.Core.Config;
using ShortRent.Core.Data;
using ShortRent.Core.Domain;
using ShortRent.Core.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Service
{
    public class CompanyService: BaseService,ICompanyService
    {
        #region Fields
        private readonly IRepository<Company> _companyRepository;
        private readonly ICacheManager _cacheManager;
        private readonly ILogger _logger;
        private readonly ApplicationConfig _config;
        private const string CompanyCacheKey = nameof(CompanyService) + nameof(Company);
        #endregion
        #region Constroctor
        public CompanyService(IRepository<Company> companyRepository, ICacheManager cacheManager, ILogger logger, ApplicationConfig config)
        {
            _companyRepository = companyRepository;
            _cacheManager = cacheManager;
            _logger = logger;
            _config = config;
        }
        #endregion
        #region  Methods
        public void CreateCompany(Company company)
        {
            _companyRepository.Insert(company);
            _cacheManager.Remove(CompanyCacheKey);
        }
        public void  UpdateCompany(Company company)
        {
            _companyRepository.Update(company);
            _cacheManager.Remove(CompanyCacheKey);
        }
        public List<Company> GetPagedCompanys(int pageSize, int pageNumber, string Name, out int total)
        {
            List<Company> models = null;
            try
            {
                Expression<Func<Company, bool>> expression = test => true;
                if (!string.IsNullOrWhiteSpace(Name))//条件
                {
                    expression = expression.And(c => c.Name.Contains(Name));
                }
                //得到是未删除的信息
                expression = expression.And(c => c.IsDelete==false);
                if (_cacheManager.Contains(CompanyCacheKey))
                {
                    var cache = _cacheManager.Get<List<Company>>(CompanyCacheKey).Where(expression.Compile());
                    models = cache.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                    total = cache.Count();
                }
                else
                {
                    var list = _companyRepository.Entitys;
                    list = list.OrderByDescending(c => c.CreateTime).ToList();
                    if (list.Any())
                    {
                        int cacheTime = GetTimeFromConfig((int)CacheTimeLev.lev1);
                        models = list.Where(expression.Compile()).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                        total = list.Where(expression.Compile()).Count();
                        _cacheManager.Set(CompanyCacheKey, list, TimeSpan.FromMinutes(cacheTime));
                    }
                    else
                    {
                        models = new List<Company>();
                        total = 0;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Debug("返回公司分页信息出错", e);
                throw e;
            }
            return models;
        }

        public Company GetCompanyById(int id)
        {
            Company model = null;
            try
            {
                if(_cacheManager.Contains(CompanyCacheKey))
                {
                    model = _cacheManager.Get<List<Company>>(CompanyCacheKey).Where(c=>c.ID==id).FirstOrDefault();
                }
                else
                {
                    model = _companyRepository.Entitys.Where(c => c.ID == id).FirstOrDefault();
                }
            }
            catch(Exception e)
            {
                _logger.Error("根据ID获得公司出现错误",e);
                throw e;
            }
            return model;
        }
        #endregion
    }
}
