using ShortRent.Core.Cache;
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
    public class RoleService:IRoleService
    {
        #region Field
        private readonly IRepository<Role> _roleRepository;
        private readonly ICacheManager _cacheManager;
        private readonly ILogger _logger;
        /// <summary>
        /// 缓存的键
        /// </summary>
        private const string RoleCacheKey = nameof(RoleService) + nameof(Role);
        private const string AdminRoleCacheKey = nameof(Role) + nameof(Role.Type) + "Admin";
        #endregion

        #region Construction
        public RoleService(IRepository<Role> roleRepository,ICacheManager cacheManager,ILogger logger)
        {
            this._roleRepository = roleRepository;
            this._cacheManager = cacheManager;
            this._logger = logger;
        }
        #endregion

        #region  Metods
        public List<Role> GetRoles()
        {
            List<Role> roles = null;
            try
            {
                if (_cacheManager.Contains(RoleCacheKey))
                {
                    roles = _cacheManager.Get<List<Role>>(RoleCacheKey);
                }
                else
                {
                    var list = _roleRepository.Entitys;
                    if (list != null)
                    {
                        roles = list.ToList();
                        _cacheManager.Set(RoleCacheKey, roles, TimeSpan.FromMinutes(30));
                    }
                    else
                    {
                        roles = new List<Role>();
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Debug(e.Message);
                throw new Exception(e.Message);
            }
            return roles;
        }
        public Role GetAdminRole(int id)
        {
            Role role=null;
            try
            {
                if (_cacheManager.Contains(AdminRoleCacheKey))
                {
                    role = _cacheManager.Get<Role>(AdminRoleCacheKey);
                }
                else
                {
                    var model = _roleRepository.GetById(id);
                    if(model!=null)
                    {
                        role = model;
                        _cacheManager.Set(AdminRoleCacheKey,role,TimeSpan.FromMinutes(30));
                    }
                }
            }
            catch(Exception e)
            {
                _logger.Debug(e.Message);
                throw new Exception(e.Message);
            }
            return role;

        }
        #endregion
    }
}
