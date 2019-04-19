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
using ShortRent.Core;

namespace ShortRent.Service
{
    class IconsInfoService:BaseService,IIconsInfoService
    {
        #region Fields
        private readonly IRepository<IconsInfo> _IconsRepository;
        private readonly ICacheManager _cacheManager;
        private readonly ILogger _logger;
        private readonly ApplicationConfig _config;
        //键值
        private const string IconsCache = nameof(IconsInfoService) + nameof(IconsInfo);
        #endregion
        #region Construction
        public IconsInfoService(IRepository<IconsInfo> iconsRepository, ICacheManager cacheManager, ILogger logger, ApplicationConfig config)
        {
            this._IconsRepository = iconsRepository;
            this._cacheManager = cacheManager;
            this._logger = logger;
            this._config = config;
        }
        #endregion
        #region Methods
        public List<IconsInfo> GetIconsInfos()
        {
            List<IconsInfo> icons = null;
            try
            {
                if (_cacheManager.Contains(IconsCache))
                {
                    icons = _cacheManager.Get<List<IconsInfo>>(IconsCache);
                }
                else
                {
                    var list = _IconsRepository.Entitys;
                    if (list.Any())
                    {
                        icons = list.ToList();
                        int cacheTime = GetTimeFromConfig((int)CacheTimeLev.lev1);
                        _cacheManager.Set(IconsCache, icons, TimeSpan.FromMinutes(cacheTime));
                    }
                }
            }
            catch(Exception e)
            {
                _logger.Error("获取图标列表出错",e);
                throw e;
            }
            return icons;
        }
        #endregion
    }
}
