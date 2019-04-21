using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using ShortRent.Core;
using ShortRent.Core.Log;
using ShortRent.Service;
using ShortRent.Web.Models;
using ShortRent.WebCore.MVC;

namespace ShortRent.Web.Controllers
{
    public class HistoryOperatorController : BaseController
    {
        #region Fields
        private readonly IHistoryOperatorService _historyOperatorService;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfig;
        private readonly ILogger _logger;
        #endregion

        #region Construction
        public HistoryOperatorController(IHistoryOperatorService historyOperatorService,
            IMapper mapper,
            MapperConfiguration mapperConfig,
            ILogger logger)
        {
            this._historyOperatorService = historyOperatorService;
            this._mapper = mapper;
            this._mapperConfig = mapperConfig;
            this._logger = logger;
        }
        #endregion
        #region Methods
        public ActionResult List()
        {
            ViewBag.Title = "系统管理";
            ViewBag.Content = "历史操作管理";
            return View();
        }
        public ActionResult Index(int pageSize,int pageNumber,string pName,string entityName)
        {
            List<HistoryOperatorViewModel> history = null;
            PagedListViewModel<HistoryOperatorViewModel> paged = new PagedListViewModel<HistoryOperatorViewModel>();
            try
            {
                int total;
                var model = _historyOperatorService.GetHistoryOperators(pageSize, pageNumber,pName,entityName, out total);
                history = _mapper.Map<List<HistoryOperatorViewModel>>(model);
                paged.Total = total;
                paged.Rows = history;
            }
            catch (Exception e)
            {
                _logger.Debug("获得历史操作记录出错", e);
                throw e;
            }
            return Json(paged);
        }
        public ActionResult Detail(int id)
        {
            ViewBag.Title = "系统管理";
            ViewBag.Content = "历史操作详情";
            HistoryOperatorDetail historyDetail = new HistoryOperatorDetail();
            try
            {
                var model = _historyOperatorService.GetHistoryOperator(id);
                historyDetail = _mapper.Map<HistoryOperatorDetail>(model);
            }
            catch (Exception e)
            {
                _logger.Debug("获取历史操作详情信息出错", e);
                throw e;
            }
            return View(historyDetail);
        }
        public ActionResult Detele(int id)
        {
            try
            {
                //删除的对象
                var model = _historyOperatorService.GetHistoryOperator(id);
                _historyOperatorService.DeleteHistory(model);
            }
            catch (Exception e)
            {
                _logger.Debug("删除历史操作出错", e);
                throw e;
            }
            return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.OK, Message = "删除历史操作成功" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}