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
    public class PublishMsgController : BaseController
    {
        #region Field 
        private readonly IPublishMsgService _publishMsgService;
        private readonly IBussinessService _bussinessService;
        private readonly IPersonService _personService;
        //autoMapper
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfig;
        private readonly ILogger _logger;
        #endregion

        #region Construction
        public PublishMsgController(IPublishMsgService publishMsgService
            ,IBussinessService bussinessService
            ,IPersonService personService
            , IMapper mapper
            ,MapperConfiguration mapperConfiguration
            ,ILogger logger)
        {
            this._publishMsgService = publishMsgService;
            this._personService = personService;
            this._bussinessService = bussinessService;
            this._mapper = mapper;
            this._mapperConfig = mapperConfiguration;
            this._logger = logger;
        }
        #endregion

        #region Methods
        public ActionResult RecruiterByList()
        {
            ViewBag.Title = "被招聘者发布信息管理";
            ViewBag.Content = "发布信息管理";
            List<Business> bussiness = _bussinessService.GetBussinesss();
            List<SelectListItem> selectListItems = new SelectList(bussiness, "ID", "Name", 0).ToList();
            selectListItems.Insert(0,new SelectListItem() { Text="请选择",Value="0"});
            ViewBag.Bussiness = selectListItems;
            return View();
        }
        public ActionResult RecruiterByIndex(int pagedIndex, int pagedSize, string name, int? bussiness, DateTime? startTime, DateTime? endTime)
        {
            List<RecruiterByUserTypePersonModel> list = null;
            PagedListViewModel<RecruiterByUserTypePersonModel> pageList = new PagedListViewModel<RecruiterByUserTypePersonModel>();
            try
            {
                int total;
                var RecruiterBy = _publishMsgService.GetPageRecruiterBy(pagedIndex, pagedSize, name,bussiness, startTime, endTime, out total);
                if (RecruiterBy.Any())
                {
                    list = _mapper.Map<List<RecruiterByUserTypePersonModel>>(RecruiterBy);
                    pageList.Total = total;
                    pageList.Rows = list;
                }
            }
            catch (Exception e)
            {
                _logger.Debug("得到分页数据出错,被招聘者发布信息", e);
                throw e;
            }
            return Json(pageList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RecruiterList()
        {
            ViewBag.Title = "招聘者发布信息管理";
            ViewBag.Content = "发布信息管理";
            List<Business> bussiness = _bussinessService.GetBussinesss();
            List<SelectListItem> selectListItems = new SelectList(bussiness, "ID", "Name", 0).ToList();
            selectListItems.Insert(0, new SelectListItem() { Text = "请选择", Value = "0" });
            ViewBag.Bussiness = selectListItems;
            return View();
        }
        public ActionResult RecruiterIndex(int pagedIndex, int pagedSize, string companyName, string name, int? bussiness, DateTime? startTime, DateTime? endTime)
        {
            List<RecruiterUserTypePersonModel> list = null;
            PagedListViewModel<RecruiterUserTypePersonModel> pageList = new PagedListViewModel<RecruiterUserTypePersonModel>();
            try
            {
                int total;
                var Recruiter = _publishMsgService.GetPageRecruiter(pagedIndex, pagedSize, companyName, name, bussiness, startTime, endTime, out total);
                if (Recruiter.Any())
                {
                    list = _mapper.Map<List<RecruiterUserTypePersonModel>>(Recruiter);
                    pageList.Total = total;
                    pageList.Rows = list;
                }
            }
            catch (Exception e)
            {
                _logger.Debug("得到分页数据出错,招聘者发布信息", e);
                throw e;
            }
            return Json(pageList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Watch(int id)
        {
            ViewBag.Title = "发布内容";
            ViewBag.Content = "被招聘者发布信息管理";
            try
            {
                PublishMsg publish = _publishMsgService.GetPublishMsgById(id);
                PublishMsgWatch Watch = _mapper.Map<PublishMsgWatch>(publish);
                return View(Watch);
            }
            catch(Exception e)
            {
                _logger.Debug("获得被招聘者发布信息详情出现错误",e);
                throw e;
            }
        }
        public ActionResult LowerCredit(int id)
        {
            try
            {
                int personId = _publishMsgService.PersonIdByPublishId(id);
                Person person = _personService.GetPerson(personId);
                person.CreditScore = person.CreditScore - 5;
                _personService.CreatePerson(person);
                return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.OK, Message = "降低发布信息出现顺序成功！", Url = Url.Action(nameof(PublishMsgController.RecruiterByList)) });
            }
            catch(Exception e)
            {
                _logger.Debug("降低发布信息出现的顺序");
                return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.InternalServerError, Url = Url.Action(nameof(SystemController.InternalServerError), "System") });
            }
        }
        public ActionResult LowerCreditCompany(int id)
        {
            try
            {
                int personId = _publishMsgService.PersonIdByPublishId(id);
                Person person = _personService.GetPerson(personId);
                person.CreditScore = person.CreditScore - 1;
                _personService.CreatePerson(person);
                return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.OK, Message = "降低发布信息出现顺序成功！", Url = Url.Action(nameof(PublishMsgController.RecruiterList)) });
            }
            catch (Exception e)
            {
                _logger.Debug("降低发布信息出现的顺序");
                return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.InternalServerError, Url = Url.Action(nameof(SystemController.InternalServerError), "System") });
            }
        }
        #endregion

    }
}