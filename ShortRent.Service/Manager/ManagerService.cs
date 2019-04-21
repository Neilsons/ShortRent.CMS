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

namespace ShortRent.Service
{
    public class ManagerService:BaseService,IManagerService
    {
        #region Fields
        private readonly IRepository<Manager> _managerRepository;
        private readonly ICacheManager _cacheManager;
        private readonly ILogger _logger;
        private readonly ApplicationConfig _config;
        //缓存的键
        private const string ManagerCancheKey = nameof(ManagerService) + nameof(Manager);
        #endregion

        #region  Construction
        public ManagerService(IRepository<Manager> managerRepository,
                              ICacheManager cacheManager,
                              ILogger logger,
                              ApplicationConfig config)
        {
            this._managerRepository = managerRepository;
            this._cacheManager = cacheManager;
            this._logger = logger;
            this._config = config;
        }
        #endregion

        #region Methods
        /// <summary>
        /// 返回所有的菜单
        /// </summary>
        /// <returns></returns>
        public List<Manager> GetManagers()
        {
            List<Manager> managers = null;
            try
            {
                if(_cacheManager.Contains(ManagerCancheKey))
                {
                    managers = _cacheManager.Get<List<Manager>>(ManagerCancheKey);
                }
                else
                {
                    var list = _managerRepository.IncludeEntitys("Childrens").OrderByDescending(c =>c.CreateTime).ToList();
                    if(list.Any())
                    {
                        managers = list;
                        int cacheTime = GetTimeFromConfig((int)CacheTimeLev.lev1);
                        _cacheManager.Set(ManagerCancheKey, list, TimeSpan.FromMinutes(cacheTime));
                    }
                }
            }
            catch(Exception e)
            {
                _logger.Error("获得全部菜单出错！",e);
                throw e;
            }
            return managers;
        }

        public List<Manager> GetTreeViewManagers()
        {
            List<Manager> treeView = null;
            try
            {
                var list = GetManagers();
                if(list.Any())
                {
                    treeView = list.Where(c=>c.Pid==null).ToList();
                }
            }
            catch(Exception e)
            {
                _logger.Error("创建菜单树形结构出错",e);
                throw e;
            }
            return treeView;
        }
        /// <summary>
        /// 创建一个菜单
        /// </summary>
        /// <param name="manager"></param>
        public void CreateManager(Manager manager)
        {
            try
            {
                if(manager==null)
                {
                    throw new NullReferenceException();
                }
                _managerRepository.Insert(manager);
                //清空缓存
                _cacheManager.Remove(ManagerCancheKey);
            }
            catch(Exception e)
            {
                _logger.Error("创建用户操作数据库过程中出错。",e);
                throw e;
            }
        }
        /// <summary>
        /// 根据id得到一个manager
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Manager GetManager(int? id)
        {
            if(id==null)
            {
                id = 0;
            }
            Manager model = null;
            try
            {
                if (_cacheManager.Contains(ManagerCancheKey))
                {
                    model = _cacheManager.Get<List<Manager>>(ManagerCancheKey).Find(c=>c.ID==id);
                }
                else
                {
                    var list = _managerRepository.Entitys;
                    if (list.Any())
                    {
                        model = list.Where(c=>c.ID==id).FirstOrDefault();
                    }
                }
            }
            catch(Exception e)
            {
                _logger.Error("获取某一个菜单失败！", e);
                throw e;
            }
            if(model==null)
            {
                model = new Manager();
            }
            return model;
        }
        /// <summary>
        /// 更新一个菜单
        /// </summary>
        /// <param name="id"></param>
        public void UpdateManager(Manager model)
        {
            try
            {
                //得到实体之后更新
                _managerRepository.Update(model);
                _cacheManager.Remove(ManagerCancheKey);
            }
            catch(Exception e)
            {
                _logger.Debug("更新表单出错，数据库方面",e);
                throw e;
            }
        }
        #endregion
    }
}
