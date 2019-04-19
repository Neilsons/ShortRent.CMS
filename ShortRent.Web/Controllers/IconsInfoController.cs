using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using ShortRent.Core.Domain;
using ShortRent.Core.Log;
using ShortRent.Service;
using ShortRent.Web.Models;
using ShortRent.WebCore.MVC;

namespace ShortRent.Web.Controllers
{
    public class IconsInfoController : BaseController
    {
        #region Fields
        private readonly IIconsInfoService _IconsService;
        private readonly IHistoryOperatorService _historyOperatorService;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfig;
        private readonly ILogger _logger;
        #endregion
        #region Construction
        public IconsInfoController(IIconsInfoService IconsService, 
            IHistoryOperatorService historyOperatorService,
            IMapper mapper,
            MapperConfiguration mapperConfig,
            ILogger logger
            )
        {
            this._IconsService = IconsService;
            this._historyOperatorService = historyOperatorService;
            this._mapper = mapper;
            this._mapperConfig = mapperConfig;
            this._logger = logger;
        }                         
        #endregion                 
        // GET: IconsInfo
        public ActionResult GetIconsInfo()
        {
            List<IconViewModel> icons = null;
            try
            {
                var model = _IconsService.GetIconsInfos();
                icons = _mapper.Map<List<IconViewModel>>(model);
            }
            catch(Exception e)
            {
                _logger.Debug("显示字体图标出错",e);
                throw e;
            }
            return View(icons);
        }
    }
}