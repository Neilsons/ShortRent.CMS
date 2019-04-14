using ShortRent.Core.Cache;
using ShortRent.Core.Data;
using ShortRent.Core;
using ShortRent.Core.Domain;
using ShortRent.Core.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Service
{
    public class PermissionService:BaseService,IPermissionService
    {
        #region Fields
        private readonly IRepository<Permission> _permissionRepository;
        private readonly ILogger _logger;
        private readonly ICacheManager _cacheManager;
        //主键
        private const string PermissionCacheKey = nameof(PermissionService) + nameof(Permission);
        #endregion
        #region Construction
        public PermissionService(IRepository<Permission> permissionReopsitory,ILogger logger,ICacheManager cacheManager)
        {
            this._permissionRepository = permissionReopsitory;
            this._logger = logger;
            this._cacheManager = cacheManager;
        }
        #endregion
        #region Methods
        /// <summary>
        /// 获取后台的所有权限
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Permission> GetPermissions()
        {
            IEnumerable<Permission> permissions = null;
            try
            {
                if (_cacheManager.Contains(PermissionCacheKey))
                {
                    permissions = _cacheManager.Get<IEnumerable<Permission>>(PermissionCacheKey);
                }
                else
                {
                    var list = _permissionRepository.Entitys;
                    if (list.Any())
                    {
                        permissions = list.Where(c => c.Type);
                        int cacheTime = GetTimeFromConfig((int)CacheTimeLev.lev1);
                        _cacheManager.Set(PermissionCacheKey, list, TimeSpan.FromMinutes(cacheTime));
                    }
                }
            }
            catch(Exception e)
            {
                _logger.Debug(e.Message);
                throw new Exception(e.Message);
            }
            return permissions;
        }
        #endregion
    }
}
