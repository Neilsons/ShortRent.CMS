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
using ShortRent.Core.Config;
using System.Linq.Expressions;
using ShortRent.Core;

namespace ShortRent.Service
{
    public class RoleService: BaseService,IRoleService
    {
        #region Field
        private readonly IRepository<Role> _roleRepository;
        private readonly ICacheManager _cacheManager;
        private readonly ILogger _logger;
        private readonly ApplicationConfig _config;
        /// <summary>
        /// 缓存的键
        /// </summary>
        private const string RoleCacheKey = nameof(RoleService) + nameof(Role);
        private const string PermissionsCacheKey = nameof(RoleService) + nameof(Permission);
        #endregion

        #region Construction
        public RoleService(IRepository<Role> roleRepository, ICacheManager cacheManager,ILogger logger,ApplicationConfig config)
        {
            this._roleRepository = roleRepository;
            this._cacheManager = cacheManager;
            this._logger = logger;
            this._config = config;
        }
        #endregion

        #region  Metods
        /// <summary>
        /// 返回所有角色
        /// </summary>
        /// <returns></returns>
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
                    if (list.Any())
                    {
                        roles = list.Where(c=>c.IsDelete==false).ToList();                        
                        int cacheTime=GetTimeFromConfig(1);
                        _cacheManager.Set(RoleCacheKey, roles, TimeSpan.FromMinutes(cacheTime));
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
        /// 返回未查询的分页的数据
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public List<Role> GetRoles(int pageSize,int pageNumber,out int total)
        {
           return GetRoles(pageSize, pageNumber,null, out total);
        }
        /// <summary>
        /// 返回查询的分页数据
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="roleName"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<Role> GetRoles(int pageSize,int pageNumber,string roleName,out int total)
        {
            List<Role> roles = null;
            try
            {
                Expression<Func<Role, bool>> expression = test => true;
                if (!string.IsNullOrWhiteSpace(roleName))//条件
                {
                   expression= expression.And(c => c.Name.Contains(roleName));
                }
                if (_cacheManager.Contains(RoleCacheKey))
                {
                    var cache = _cacheManager.Get<List<Role>>(RoleCacheKey).Where(expression.Compile());
                    roles = cache.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                    total = cache.Count();
                }
                else
                {
                    var list = _roleRepository.Entitys;
                    if (list.Any())
                    {
                        list = list.Where(c => c.IsDelete == false).ToList();
                        int cacheTime = GetTimeFromConfig(1);
                        roles = list.Where(expression.Compile()).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                        total = list.Count();
                        _cacheManager.Set(RoleCacheKey, list, TimeSpan.FromMinutes(cacheTime));
                    }
                    else
                    {
                        roles = new List<Role>();
                        total = 0;
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
                    _cacheManager.Get<List<Role>>(RoleCacheKey).Where(c => c.Type == true && c.ID == id).FirstOrDefault();
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
        /// 返回某一角色的权限列表列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Permission> GetPermissions(int id)
        {
            List<Permission> permissions= null;
            try
            {
                if (_cacheManager.Contains(PermissionsCacheKey))
                {
                    permissions = _cacheManager.Get<List<Permission>>(PermissionsCacheKey);
                }
                else
                {
                    var list = GetAdminRole(id).Permissions;
                    if (list.Any())
                    {
                        permissions = list.ToList();
                        int cacheTime = GetTimeFromConfig(1);
                        _cacheManager.Set(PermissionsCacheKey, permissions, TimeSpan.FromMinutes(cacheTime));
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
