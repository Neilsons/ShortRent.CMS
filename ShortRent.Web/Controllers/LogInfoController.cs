using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShortRent.Core.Log;
using ShortRent.Service;
using ShortRent.WebCore.MVC;
using ShortRent.Web.Models;
using AutoMapper;

namespace ShortRent.Web.Controllers
{
    public class LogInfoController : BaseController
    {
        #region Fields
        private readonly ILogInfoService _logInfoService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfig;
        #endregion
        #region Construction
        public LogInfoController(ILogInfoService logInfoService,ILogger logger,IMapper mapper,MapperConfiguration mapperConfig)
        {
            this._logger = logger;
            this._logInfoService = logInfoService;
            this._mapper = mapper;
            this._mapperConfig = mapperConfig;
        }
        #endregion
        #region Methods
        public ActionResult List()
        {
            ViewBag.Title = "系统管理";
            ViewBag.Content = "日志列表";
            return View();
        }
        /// <summary>
        /// 返回分页的数据项
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public ActionResult Index(int? pageSize, int? pageNumber)
        {
            List<LogViewModelIndex> list = null;
            PagedListViewModel<LogViewModelIndex> pageList =new PagedListViewModel<LogViewModelIndex>();
            try
            {
                int total;
                var logInfos = _logInfoService.GetLogPagedListInfo(pageNumber??0,pageSize??0, out total);
                if (logInfos.Any())
                {
                    list = _mapper.Map<List<LogViewModelIndex>>(logInfos);
                    pageList.Total = total;
                    pageList.Rows = list;
                }
            }
            catch(Exception e)
            {
                _logger.Debug("得到分页数据出错，控制器下",e);
                throw e;
            }
            return Json(pageList,JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取详情信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detail(int id)
        {
            ViewBag.Title = "系统管理";
            ViewBag.Content = "日志详情";
            LogDetailViewModel logDetailVm = new LogDetailViewModel();
            try
            {
                var loginfo = _logInfoService.GetDetail(id);             
                LogDetailChange logChange =_mapper.Map<LogDetailChange>(loginfo);
                logDetailVm.StachTrace = logChange.StachTrace;
                logDetailVm.Exception = GetObjectByJson<LogInfoException>(logChange.Exception);
            }
            catch(Exception e)
            {
                _logger.Debug("获取日志详情信息出错",e);
                throw e;
            }
            return View(logDetailVm);
        }
        #endregion

    }
}