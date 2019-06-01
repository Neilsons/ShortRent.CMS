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
    public class EntityPermissionService:BaseService,IEntityPermissionService
    {
        #region Field
        private readonly IRepository<EntityPermission> _entityRepository;
        private readonly ICacheManager _cacheManager;
        private readonly ILogger _logger;
        private readonly ApplicationConfig _config;
        /// <summary>
        /// 缓存的键
        /// </summary>
        private const string EntityCacheKey = nameof(EntityPermissionService) + nameof(EntityPermission);
        #endregion

        #region Construction
        public EntityPermissionService(IRepository<EntityPermission> entityRepository, ICacheManager cacheManager, ILogger logger, ApplicationConfig config)
        {
            this._entityRepository = entityRepository;
            this._cacheManager = cacheManager;
            this._logger = logger;
            this._config = config;
        }
        #endregion
        #region Methods
        public void CreateEntity(EntityPermission model)
        {
            _entityRepository.Insert(model);
            _cacheManager.Remove(EntityCacheKey);
        }
        public List<EntityPermission> GetEntityPermissions()
        {
            List<EntityPermission> models = null;
            try
            {
                if (_cacheManager.Contains(EntityCacheKey))
                {
                    var cache = _cacheManager.Get<List<EntityPermission>>(EntityCacheKey);
                    models = cache;
                }
                else
                {
                    var list = _entityRepository.Entitys;
                    models = list.OrderByDescending(c => c.CreateTime).ToList();
                    if (list.Any())
                    {
                        int cacheTime = GetTimeFromConfig((int)CacheTimeLev.lev1);
                        _cacheManager.Set(EntityCacheKey, models, TimeSpan.FromMinutes(cacheTime));
                    }
                    else
                    {
                        models = new List<EntityPermission>();
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Debug("返回实体信息出错", e);
                throw e;
            }
            return models;
        }
        public void DeleteEntity(EntityPermission model)
        {
            _entityRepository.Delete(model);
            _cacheManager.Remove(EntityCacheKey);
        }
        #endregion
    }
}
