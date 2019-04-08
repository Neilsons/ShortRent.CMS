using ShortRent.Core.Cache;
using ShortRent.Core.Data;
using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Service
{
    public class RoleService:IRoleService
    {
        #region Field
        private readonly IRepository<Role> _roleRepository;
        private readonly ICacheManager _cacheManager;
        /// <summary>
        /// 缓存的键
        /// </summary>
        private const string RoleCacheKey = nameof(RoleService) + nameof(Role);
        #endregion

        #region Construction
        public RoleService(IRepository<Role> roleRepository,ICacheManager cacheManager)
        {
            this._roleRepository = roleRepository;
            this._cacheManager = cacheManager;
        }
        #endregion

        #region  Metods
        public List<Role> GetRoles()
        {
            List<Role> roles = null;
            if(_cacheManager.Contains(RoleCacheKey))
            {
                roles = _cacheManager.Get<List<Role>>(RoleCacheKey);
            }
            else
            {
                var list = _roleRepository.Entitys;
                if(list!=null)
                {
                    roles = list.ToList();
                    _cacheManager.Set(RoleCacheKey, roles, TimeSpan.FromMinutes(30));
                }
                else
                {
                    roles = new List<Role>();
                }
            }
            return roles;
        }
        #endregion
    }
}
