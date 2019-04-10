using ShortRent.Core.Cache;
using ShortRent.Core.Data;
using ShortRent.Core.Domain;
using ShortRent.Core.Log;
using ShortRent.Data;
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
        private const string PermissionsCacheKey = nameof(RoleService) + nameof(Permission);
        #endregion

        #region Construction
        public RoleService(IRepository<Role> roleRepository, ICacheManager cacheManager,ILogger logger)
        {
            this._roleRepository = roleRepository;
            this._cacheManager = cacheManager;
            this._logger = logger;
        }
        #endregion

        #region  Metods
        public IQueryable<Role> GetRoles()
        {
            IQueryable<Role> roles = null;
            try
            {
                if (_cacheManager.Contains(RoleCacheKey))
                {
                    roles = _cacheManager.Get<IQueryable<Role>>(RoleCacheKey);
                }
                else
                {
                    var list = _roleRepository.Entitys;
                    if (list.Any())
                    {
                        roles = list.Where(c=>c.IsDelete==false);
                        _cacheManager.Set(RoleCacheKey, roles, TimeSpan.FromMinutes(30));
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
        /// <summary>
        /// 返回后台用户角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Role GetAdminRole(int id)
        {
            Role role=null;
            try
            {
                if (_cacheManager.Contains(RoleCacheKey))
                {
                    _cacheManager.Get<IQueryable<Role>>(RoleCacheKey).Where(c => c.Type == true && c.ID == id).FirstOrDefault();
                }
                else
                {
                    //类型为true 的是后台用户
                    var model = _roleRepository.Entitys.Where(c => c.ID == id && c.IsDelete == false && c.Type).FirstOrDefault();
                    if(model!=null)
                    {
                        role = model;
                    }
                    else
                    {
                        role = new Role();
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
        /// <summary>
        /// 返回某一角色的列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<Permission> GetPermissions(int id)
        {
            IQueryable <Permission> permissions= null;
            try
            {
                if (_cacheManager.Contains(PermissionsCacheKey))
                {
                    permissions = _cacheManager.Get<IQueryable<Permission>>(PermissionsCacheKey);
                }
                else
                {
                    var list = GetAdminRole(id).Permissions.AsQueryable();
                    if (list.Any())
                    {
                        permissions = list;
                        _cacheManager.Set(PermissionsCacheKey, permissions, TimeSpan.FromMinutes(30));
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
