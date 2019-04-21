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
                    var list = _roleRepository.Entitys.OrderByDescending(c=>c.CreateTime);
                    if (list.Any())
                    {
                        roles = list.Where(c=>c.IsDelete==false).ToList();                        
                        int cacheTime=GetTimeFromConfig((int)CacheTimeLev.lev1);
                        _cacheManager.Set(RoleCacheKey, roles, TimeSpan.FromMinutes(cacheTime));
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Debug("返回所有角色", e);
                throw e;
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
                    if(pageSize==0&&pageNumber==0)
                    {
                        roles = cache.ToList();
                    }
                    else
                    {
                        roles = cache.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                    }
                    total = cache.Count();
                }
                else
                {
                    var list = _roleRepository.Entitys;
                    list = list.OrderByDescending(c => c.CreateTime).Where(c => c.IsDelete == false).ToList();
                    if (list.Any())
                    {
                        int cacheTime = GetTimeFromConfig((int)CacheTimeLev.lev1);
                        if (pageSize == 0 && pageNumber == 0)
                        {
                            roles = list.Where(expression.Compile()).ToList();
                        }
                        else
                        {
                            roles = list.Where(expression.Compile()).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                        }
                        total = list.Where(expression.Compile()).Count();
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
                _logger.Debug("返回查询的分页数据", e); 
                throw e;
            }
            return roles;

        }
        /// <summary>
        /// 返回用户角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Role GetRole(int id,bool? type=null)
        {
            Role role=null;
            try
            {
                if (_cacheManager.Contains(RoleCacheKey))
                {
                    if (type == null)
                     role=_cacheManager.Get<List<Role>>(RoleCacheKey).Where( c=>c.ID == id).FirstOrDefault();
                    else
                    role=_cacheManager.Get<List<Role>>(RoleCacheKey).Where(c => c.Type == type && c.ID == id).FirstOrDefault();
                }
                else
                {
                    //类型为true 的是后台用户
                    if(type==null)
                    role = _roleRepository.Entitys.Where(c => c.ID == id && c.IsDelete == false).FirstOrDefault();
                    else
                    role = _roleRepository.Entitys.Where(c => c.ID == id && c.IsDelete == false && c.Type == type).FirstOrDefault();
                    if (role==null)
                    {
                        role = new Role();
                    }
                }
            }
            catch(Exception e)
            {
                _logger.Debug("返回某一个用户角色", e);
                throw e;
            }
            return role;

        }
        /// <summary>
        /// 返回某一角色的权限列表
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
                    permissions = GetRole(id,true).Permissions.ToList();
                    if (permissions.Any())
                    {
                        int cacheTime = GetTimeFromConfig((int)CacheTimeLev.lev1);
                        _cacheManager.Set(PermissionsCacheKey, permissions, TimeSpan.FromMinutes(cacheTime));
                    }
                }
            }
            catch(Exception e)
            {
                _logger.Debug("返回某一角色的权限列表", e);
                throw e;
            }          
            return permissions;
        }
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="role"></param>
        public void CreateRole(Role role)
        {
            try
            {
                _roleRepository.Insert(role);
                _cacheManager.Remove(RoleCacheKey);
            }
            catch(Exception e)
            {
                _logger.Debug("创建角色", e);
                throw e;
            }
        }
        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="role"></param>
        public void UpdateRole(Role role)
        {
            try
            {
                Role oldRole = GetRole(role.ID);
                role.CreateTime = oldRole.CreateTime;
                _roleRepository.Update(role);
                _cacheManager.Remove(RoleCacheKey);
            }
            catch(Exception e)
            {
                _logger.Debug("更新角色", e);
                throw e;
            }
        }
        #endregion
    }
}
