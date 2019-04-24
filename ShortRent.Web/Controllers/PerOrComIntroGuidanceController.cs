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
using ShortRent.WebCore.MVC;

namespace ShortRent.Web.Controllers
{
    public class PerOrComIntroGuidanceController : BaseController
    {
        #region Fields
        private readonly IPerOrComIntroGuidanceService _iPerOrComIntroGuidanceService;
        private readonly IHistoryOperatorService _historyOperatorService;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfig;
        private readonly ILogger _logger;
        #endregion

        #region Construction
        public PerOrComIntroGuidanceController(IPerOrComIntroGuidanceService iPerOrComIntroGuidance,
            IHistoryOperatorService historyOperatorService,
            IMapper mapper,
            MapperConfiguration mapperConfig,
            ILogger logger
            )
        {
            this._iPerOrComIntroGuidanceService = iPerOrComIntroGuidance;
            this._historyOperatorService = historyOperatorService;
            this._mapper = mapper;
            this._mapperConfig = mapperConfig;
            this._logger = logger;
        }
        #endregion

        #region Methods
        public ActionResult List()
        {
            ViewBag.Title = "介绍问题管理";
            ViewBag.Content = "信息列表";
            return View();
        }
        public ActionResult Index(int pageSize,int pageNumber,bool? Type)
        {
            List<PerOrComIntroGuidanceViewModel> history = null;
            PagedListViewModel<PerOrComIntroGuidanceViewModel> paged = new PagedListViewModel<PerOrComIntroGuidanceViewModel>();
            try
            {
                int total;
                var model = _iPerOrComIntroGuidanceService.GetPagedPerOrComIntor(pageSize, pageNumber, Type, out total);
                history = _mapper.Map<List<PerOrComIntroGuidanceViewModel>>(model);
                paged.Total = total;
                paged.Rows = history;
            }
            catch (Exception e)
            {
                _logger.Debug("获得介绍问题出错", e);
                throw e;
            }
            return Json(paged);
        }
        public ActionResult Create()
        {
            ViewBag.Title = "介绍问题管理";
            ViewBag.Content = "信息创建";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PerOrComIntroGuidanceViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PerOrComIntroGuidance intro = _mapper.Map<PerOrComIntroGuidance>(model);
                    intro.CreateTime = DateTime.Now;
                    _iPerOrComIntroGuidanceService.Insert(intro);
                    //记入历史记录代码
                    model.ID = intro.ID;
                    model.CreateTime = intro.CreateTime;
                    PerOrComIntroGuidanceHumanModel hisModel = _mapper.Map<PerOrComIntroGuidanceHumanModel>(model);
                    //记入历史操作表
                    HistoryOperator history = new HistoryOperator()
                    {
                        CreateTime = DateTime.Now,
                        DetailDescirption = GetDescription<PerOrComIntroGuidanceHumanModel>("创建了一个介绍问题，详情", hisModel),
                        EntityModule = "系统管理",
                        Operates = "介绍问题创建",
                        PersonId = GetCurrentPerson().ID
                    };
                    _historyOperatorService.CreateHistoryOperator(history);
                    return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.OK, Message = "介绍问题创建成功", Url = Url.Action(nameof(PerOrComIntroGuidanceController.List)) });
                }
            }
            catch (Exception e)
            {
                _logger.Debug("创建角色表单提交", e); ;
                throw e;
            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "编辑问题介绍";
            ViewBag.Content = "编辑问题介绍表单";
            PerOrComIntroGuidanceViewModel model = null;
            try
            {
                PerOrComIntroGuidance intro = _iPerOrComIntroGuidanceService.GetIntro(id);
                model = _mapper.Map<PerOrComIntroGuidanceViewModel>(intro);
                if (model == null)
                {
                    model = new PerOrComIntroGuidanceViewModel();
                }
            }
            catch (Exception e)
            {
                _logger.Debug("编辑介绍问题显示界面出错", e); ;
                throw e;
            }
            return View("Create", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PerOrComIntroGuidanceViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PerOrComIntroGuidance intro = _mapper.Map<PerOrComIntroGuidance>(model);
                    //获取之前内容
                    PerOrComIntroGuidance oldRole = _iPerOrComIntroGuidanceService.GetIntro(model.ID);
                    //先转换为viewmodel
                    var oldviewModel = _mapper.Map<PerOrComIntroGuidanceViewModel>(oldRole);
                    _iPerOrComIntroGuidanceService.Update(intro);
                    //得到转换后的人性化模型
                    PerOrComIntroGuidanceHumanModel hisModel = _mapper.Map<PerOrComIntroGuidanceHumanModel>(model);
                    PerOrComIntroGuidanceHumanModel oldRoleHum = _mapper.Map<PerOrComIntroGuidanceHumanModel>(oldviewModel);
                    HistoryOperator history = new HistoryOperator()
                    {
                        CreateTime = DateTime.Now,
                        DetailDescirption = GetDescription<PerOrComIntroGuidanceHumanModel>("编辑了一个介绍问题，详情", hisModel, oldRoleHum),
                        EntityModule = "介绍问题",
                        Operates = "编辑",
                        PersonId = GetCurrentPerson().ID,
                    };
                    _historyOperatorService.CreateHistoryOperator(history);
                    return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.OK, Message = "介绍问题编辑成功", Url = Url.Action(nameof(PerOrComIntroGuidanceController.List)) });
                }
            }
            catch (Exception e)
            {
                _logger.Debug("编辑角色提交", e);
                throw e;
            }
            return View("Create", model);
        }
        #endregion
    }
}