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
    public class BussinessController : BaseController
    {
        #region Fields
        private readonly IBussinessService _bussinessService;
        private readonly IHistoryOperatorService _historyOperatorService;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfig;
        private readonly ILogger _logger;
        #endregion

        #region Contruction
        public BussinessController(IBussinessService bussinessService,
            IHistoryOperatorService historyOperatorService,
            IMapper mapper,
            MapperConfiguration mapperConfig,
            ILogger logger)
        {
            this._bussinessService = bussinessService;
            this._historyOperatorService = historyOperatorService;
            this._mapper = mapper;
            this._mapperConfig = mapperConfig;
            this._logger = logger;
        }
        #endregion
        #region Methods
        public ActionResult Create()
        {
            ViewBag.Title = "创建";
            ViewBag.Content = "行业管理";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BussinessIndex model)
        {
            try
            {
                var bussiness = _mapper.Map<Business>(model);
                bussiness.CreateTime = DateTime.Now;
                _bussinessService.CreateBussiness(bussiness);
                //创建人性化的类
                BussinessHuman human = _mapper.Map<BussinessHuman>(bussiness);
                //创建完之后更新到历史记录中去
                HistoryOperator history = new HistoryOperator()
                {
                    CreateTime = DateTime.Now,
                    EntityModule = "信息管理",
                    DetailDescirption = GetDescription<BussinessHuman>("创建了一个行业，详情", human),
                    Operates = "创建",
                    PersonId = GetCurrentPerson().ID,
                };
                _historyOperatorService.CreateHistoryOperator(history);
            }
            catch (Exception e)
            {
                _logger.Debug("创建行业提交出错！", e);
                throw e;
            }
            return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.OK, Message = "创建行业成功！", Url = Url.Action(nameof(BussinessController.List)) });
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "编辑行业";
            ViewBag.Content = "编辑行业表单";
            BussinessIndex model = null;
            try
            {
                Business icons = _bussinessService.GetBussiness(id);
                model = _mapper.Map<BussinessIndex>(icons);
                if (model == null)
                {
                    model = new BussinessIndex();
                }
            }
            catch (Exception e)
            {
                _logger.Debug("编辑行业显示界面", e); ;
                throw e;
            }
            return View("Create", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BussinessIndex model)
        {
            try
            {
                    Business bus = _mapper.Map<Business>(model);
                    //获取之前的用户
                    Business oldModel = _bussinessService.GetBussiness(model.ID);
                    //先转换为viewmodel
                    var oldviewModel = _mapper.Map<BussinessIndex>(oldModel);
                    _bussinessService.UpdateBussiness(bus);
                    //得到转换后的人性化模型
                    BussinessHuman hisModel = _mapper.Map<BussinessHuman>(bus);
                    BussinessHuman oldRoleHum = _mapper.Map<BussinessHuman>(oldModel);
                    HistoryOperator history = new HistoryOperator()
                    {
                        CreateTime = DateTime.Now,
                        DetailDescirption = GetDescription<BussinessHuman>("编辑了一个行业，详情", hisModel, oldRoleHum),
                        EntityModule = "信息管理",
                        Operates = "编辑行业",
                        PersonId = GetCurrentPerson().ID,
                    };
                    _historyOperatorService.CreateHistoryOperator(history);
                    return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.OK, Message = "行业编辑成功", Url = Url.Action(nameof(BussinessController.List)) });
            }
            catch (Exception e)
            {
                _logger.Debug("编辑行业提交", e);
                return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.OK, Message = "联系管理员", Url = Url.Action(nameof(SystemController.InternalServerError)) });
            }
        }
        public ActionResult List()
        {
            ViewBag.Title = "行业管理";
            ViewBag.Content = "信息管理";
            return View();
        }
        public ActionResult Index(int pageSize, int pageNumber)
        {
            List<BussinessIndex> list = null;
            //返回的数据
            PagedListViewModel<BussinessIndex> pageList = new PagedListViewModel<BussinessIndex>();
            try
            {
                int total;
                var models = _bussinessService.GetPagedBussiness(pageSize, pageNumber, out total);
                if (models.Any())
                {

                    list = _mapper.Map<List<BussinessIndex>>(models);
                    pageList.Total = total;
                    pageList.Rows = list;
                }
            }
            catch (Exception e)
            {
                _logger.Debug("行业列表出错", e);
            }
            //获取所有的角色列表展示
            return Json(pageList, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}