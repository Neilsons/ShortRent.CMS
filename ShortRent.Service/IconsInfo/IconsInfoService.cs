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
using System.Linq.Expressions;

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
        public List<IconsInfo> GetIconsInfosByTotal(int pageSize, int pageNumber, string contentName,out int total)
        {
            List<IconsInfo> icons = null;
            try
            {
                Expression<Func<IconsInfo,bool>> expression=test=>true;
                if (!string.IsNullOrWhiteSpace(contentName))//条件
                {
                    expression = expression.And(c => c.Content.Contains(contentName));
                }
                if (_cacheManager.Contains(IconsCache))
                {
                    var model = _cacheManager.Get<List<IconsInfo>>(IconsCache).Where(expression.Compile());
                    if(pageSize==0&&pageNumber==0)
                    {
                        icons = model.ToList();
                    }
                    else
                    {
                        icons = model.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
                    }
                    total = model.Count();
                }
                else
                {
                    var list = _IconsRepository.Entitys.ToList();
                    if (list.Any())
                    {
                        if(pageNumber==0&&pageSize==0)
                        {
                            icons = list.Where(expression.Compile()).ToList();
                        }
                       else
                        {
                            icons = list.Where(expression.Compile()).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
                        }
                        total = list.Where(expression.Compile()).Count();
                        int cacheTime = GetTimeFromConfig((int)CacheTimeLev.lev1);
                        _cacheManager.Set(IconsCache, list, TimeSpan.FromMinutes(cacheTime));
                    }
                    else
                    {
                        total = 0;
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
        //
        public List<IconsInfo> GetIconsInfos()
        {
            int total;
            return GetIconsInfosByTotal(0,0,null,out total);
        }
        public void CreateIcon(IconsInfo model)
        {
            try
            {
                _IconsRepository.Insert(model);
                _cacheManager.Remove(IconsCache);
            }
            catch(Exception e)
            {
                _logger.Error("创建图标业务方面出错！");
                throw e;
            }
        }
        public void UpdateIcon(IconsInfo model)
        {
            try
            {
                _IconsRepository.Update(model);
                _cacheManager.Remove(IconsCache);
            }
            catch (Exception e)
            {
                _logger.Error("编辑图标业务方面出错！");
                throw e;
            }
        }
        public void Delete(IconsInfo model)
        {
            try
            {
                _IconsRepository.Delete(model);
                _cacheManager.Remove(IconsCache);
            }
            catch (Exception e)
            {
                _logger.Error("删除图标业务方面出错！");
                throw e;
            }
        }
        public IconsInfo GetIconsById(int id)
        {
            IconsInfo model = null;
            try
            {
                if (_cacheManager.Contains(IconsCache))
                {
                    model = _cacheManager.Get<List<IconsInfo>>(IconsCache).Find(c => c.ID == id);
                }
                else
                {
                    var list = _IconsRepository.Entitys;
                    if (list.Any())
                    {
                        model = list.Where(c => c.ID == id).FirstOrDefault();
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error("获取一个图标失败！", e);
                throw e;
            }
            if (model == null)
            {
                model = new IconsInfo();
            }
            return model;
        }
        #endregion
    }
}
