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
using ShortRent.Core.Domain;
using ShortRent.Core;
using System.Net;

namespace ShortRent.Web.Controllers
{
    public class LogInfoController : BaseController
    {
        #region Fields
        private readonly ILogInfoService _logInfoService;
        private readonly IHistoryOperatorService _historyOperatorService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfig;
        #endregion
        #region Construction
        public LogInfoController(ILogInfoService logInfoService,IHistoryOperatorService historyOperatorService,ILogger logger,IMapper mapper,MapperConfiguration mapperConfig)
        {
            this._logger = logger;
            this._logInfoService = logInfoService;
            this._historyOperatorService = historyOperatorService;
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
        public ActionResult Index(int? pageSize, int? pageNumber,string machineName,string catalog,DateTime? startTime,DateTime? endTime)
        {
            List<LogViewModelIndex> list = null;
            PagedListViewModel<LogViewModelIndex> pageList =new PagedListViewModel<LogViewModelIndex>();
            try
            {
                int total;
                var logInfos = _logInfoService.GetLogPagedListInfo(pageNumber??0,pageSize??0,machineName,catalog,startTime,endTime, out total);
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
                if(logChange.Exception=="")
                {
                    return Content("<script>alert('该日志下没有详情信息');window.location.href='/LogInfo/List';</Script>");
                }
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
        public ActionResult Delete(int id)
        {
            try
            {
                //删除这个日志之后得到这个日志信息
                var logInfo = _logInfoService.DeleteById(id);
                //删除一个角色之后就需要往历史记录中插入一条历史
                //得到要展示的那个实体
                LogHumanModel logHuman = _mapper.Map<LogHumanModel>(logInfo);
                HistoryOperator historyOperator = new HistoryOperator()
                {
                    CreateTime = DateTime.Now,
                    DetailDescirption = GetDescription<LogHumanModel>("删除了一个日志记录，详情", logHuman),
                    EntityModule = "系统管理",
                    Operates = "删除",
                    PersonId = GetCurrentPerson().ID,
                };
                //插入记录
                _historyOperatorService.CreateHistoryOperator(historyOperator);
            }
            catch(Exception e)
            {
                _logger.Debug("删除日志信息出错",e);
                throw e;
            }
           return Json(new AjaxJson() { HttpCodeResult=(int)HttpStatusCode.OK,Message="删除日志成功"},JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}