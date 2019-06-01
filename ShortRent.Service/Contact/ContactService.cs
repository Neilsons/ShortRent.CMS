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
    public class ContactService:BaseService,IContactService
    {
        #region Field
        private readonly IRepository<Contact> _contactRepository;
        private readonly ICacheManager _cacheManager;
        private readonly ILogger _logger;
        private readonly ApplicationConfig _config;
        /// <summary>
        /// 缓存的键
        /// </summary>
        private const string ContactCacheKey = nameof(ContactService) + nameof(Contact);
        #endregion

        #region Construction
        public ContactService(IRepository<Contact> contactRepository, ICacheManager cacheManager, ILogger logger, ApplicationConfig config)
        {
            this._contactRepository = contactRepository;
            this._cacheManager = cacheManager;
            this._logger = logger;
            this._config = config;
        }
        #endregion
        #region Methods
        public void CreateContact(Contact model)
        {
            _contactRepository.Insert(model);
            _cacheManager.Remove(ContactCacheKey);
        }
        public List<Contact> GetContacts(int pageSize, int pageNumber, DateTime? startTime, DateTime? endTime, out int total)
        {
            List<Contact> models = null;
            try
            {
                Expression<Func<Contact, bool>> expression = test => true;
                if (startTime != null)
                {
                    expression = expression.And(c => c.CreateTime >= startTime);
                }
                if (endTime != null)
                {
                    expression = expression.And(c => c.CreateTime <= endTime);
                }
                if (_cacheManager.Contains(ContactCacheKey))
                {
                    var cache = _cacheManager.Get<List<Contact>>(ContactCacheKey).Where(expression.Compile());
                    models = cache.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                    total = cache.Count();
                }
                else
                {
                    var list = _contactRepository.Entitys;
                    list = list.OrderByDescending(c => c.CreateTime).ToList();
                    if (list.Any())
                    {
                        int cacheTime = GetTimeFromConfig((int)CacheTimeLev.lev1);
                        models = list.Where(expression.Compile()).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                        total = list.Where(expression.Compile()).Count();
                        _cacheManager.Set(ContactCacheKey, list, TimeSpan.FromMinutes(cacheTime));
                    }
                    else
                    {
                        models = new List<Contact>();
                        total = 0;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Debug("返回联系信息出错", e);
                throw e;
            }
            return models;
        }
        #endregion
    }
}
