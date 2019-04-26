using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using ShortRent.Core;
using ShortRent.Core.Domain;
using ShortRent.Core.Log;
using ShortRent.Service;
using ShortRent.Web.Models;

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
        //菜单管理需要的字体图标
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
        /// <summary>
        /// 展示字体图标列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            ViewBag.Title = "系统管理";
            ViewBag.Content = "图标管理";
            return View();
        }
        /// <summary>
        /// 从字体图标获取分页数据
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="contentName"></param>
        /// <returns></returns>
        public ActionResult Index(int pageSize, int pageNumber,string contentName)
        {
            List<IconInfoShowModel> icons = null;
            PagedListViewModel<IconInfoShowModel> paged = new PagedListViewModel<IconInfoShowModel>();
            try
            {
                int total;
                var model = _IconsService.GetIconsInfosByTotal(pageSize, pageNumber, contentName, out total);
                icons = _mapper.Map<List<IconInfoShowModel>>(model);
                paged.Total = total;
                paged.Rows = icons;
            }
            catch (Exception e)
            {
                _logger.Debug("获得字体图标出错", e);
                throw e;
            }
            return Json(paged);
        }
        public ActionResult Create()
        {
            ViewBag.Title = "图标列表";
            ViewBag.Content = "图标添加";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IconViewModel model)
        {
            try
            {
                var iconInfo = _mapper.Map<IconsInfo>(model);
                _IconsService.CreateIcon(iconInfo);
                //创建人性化的类
                IconHuman human = _mapper.Map<IconHuman>(iconInfo);
                //创建完之后更新到历史记录中去
                HistoryOperator history = new HistoryOperator()
                {
                    CreateTime = DateTime.Now,
                    EntityModule = "系统管理",
                    DetailDescirption = GetDescription<IconHuman>("创建了一个图标，详情",human),
                    Operates="创建",
                    PersonId=GetCurrentPerson().ID,
                };
                _historyOperatorService.CreateHistoryOperator(history);
            }
            catch(Exception e)
            {
                _logger.Debug("创建图标提交出错！", e);
                throw e;
            }
            return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.OK, Message = "创建图标成功！", Url = Url.Action(nameof(IconsInfoController.List)) });
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "编辑图标";
            ViewBag.Content = "编辑图标表单";
            IconViewModel model = null;
            try
            {
                IconsInfo icons = _IconsService.GetIconsById(id);
                model = _mapper.Map<IconViewModel>(icons);
                if (model == null)
                {
                    model = new IconViewModel();
                }
            }
            catch (Exception e)
            {
                _logger.Debug("编辑图标显示界面", e); ;
                throw e;
            }
            return View("Create", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IconViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IconsInfo icon = _mapper.Map<IconsInfo>(model);
                    //获取之前的用户
                    IconsInfo oldModel = _IconsService.GetIconsById(model.ID);
                    //先转换为viewmodel
                    var oldviewModel = _mapper.Map<IconViewModel>(oldModel);
                    _IconsService.UpdateIcon(icon);
                    //得到转换后的人性化模型
                    IconHuman hisModel = _mapper.Map<IconHuman>(icon);
                    IconHuman oldRoleHum = _mapper.Map<IconHuman>(oldModel);
                    HistoryOperator history = new HistoryOperator()
                    {
                        CreateTime = DateTime.Now,
                        DetailDescirption = GetDescription<IconHuman>("编辑了一个图标，详情", hisModel, oldRoleHum),
                        EntityModule = "系统管理",
                        Operates = "编辑图标",
                        PersonId = GetCurrentPerson().ID,
                    };
                    _historyOperatorService.CreateHistoryOperator(history);
                    return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.OK, Message = "图标编辑成功", Url = Url.Action(nameof(IconsInfoController.List)) });
                }
            }
            catch (Exception e)
            {
                _logger.Debug("编辑图标提交", e);
                throw e;
            }
            return View("Create", model);
        }
        public ActionResult Delete(int id)
        {
            try
            {
                //得到图标信息
                var iconInfo = _IconsService.GetIconsById(id);
                _IconsService.Delete(iconInfo);
                //删除一个图标之后就需要往历史记录中插入一条历史
                //得到要展示的那个实体
                IconHuman iconHuman = _mapper.Map<IconHuman>(iconInfo);
                HistoryOperator historyOperator = new HistoryOperator()
                {
                    CreateTime = DateTime.Now,
                    DetailDescirption = GetDescription<IconHuman>("删除了一个图标，详情", iconHuman),
                    EntityModule = "系统管理",
                    Operates = "删除",
                    PersonId = GetCurrentPerson().ID,
                };
                //插入记录
                _historyOperatorService.CreateHistoryOperator(historyOperator);
            }
            catch (Exception e)
            {
                _logger.Debug("删除图标出错", e);
                throw e;
            }
            return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.OK, Message = "删除图标成功" }, JsonRequestBehavior.AllowGet);
        }
    }
}