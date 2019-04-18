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
        public ManagerService(IRepository<Manager> managerRepository, ICacheManager cacheManager, ILogger logger, ApplicationConfig config)
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
                    var list = _managerRepository.Entitys.OrderByDescending(c =>c.CreateTime).ToList();
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
        #endregion
    }
}
