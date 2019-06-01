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
using ShortRent.Web.Areas.ShortWeb.Models;
using ShortRent.WebCore.MVC;

namespace ShortRent.Web.Areas.ShortWeb.Controllers
{
    public class WebController : BaseController
    {
        #region Fields
        private readonly IContactService _contactService;
        private readonly IPublishMsgService _publishMsgService;
        private readonly IBussinessService _bussinessService;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfig;
        private readonly ILogger _logger;
        #endregion
        #region Constroctor
        public WebController(IContactService contactService,
            IPublishMsgService publishMsgService,
            IBussinessService bussinessService,
            IMapper mapper,
            MapperConfiguration mapperconfig,
            ILogger logger)
        {
            _contactService = contactService;
            _publishMsgService = publishMsgService;
            _bussinessService = bussinessService;
            _mapper = mapper;
            _mapperConfig = mapperconfig;
            _logger = logger;
        }
        #endregion
        #region Methods
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel model)
        {
            try
            {
                Contact contact = _mapper.Map<Contact>(model);
                contact.CreateTime = DateTime.Now;
                _contactService.CreateContact(contact);
            }
            catch (Exception e)
            {
                _logger.Debug("创建联系信息出错", e);
                throw e;
            }
            return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.OK, Message = "提交成功！", Url = Url.Action(nameof(WebController.Contact)) });
        }
        /// <summary>
        /// 发布职位信息
        /// </summary>
        /// <returns></returns>
        public ActionResult PublishMsg()
        {
            ViewBag.Title = "发布职位";
            List<Business> bussiness = _bussinessService.GetBussinesss();
            List<SelectListItem> selectListItems = new SelectList(bussiness, "ID", "Name",0).ToList();
            ViewBag.Bussiness = selectListItems;
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult PublishMsg(PublishCreateModel model)
        {
            try
            {
                PublishMsg publishMsg = _mapper.Map<PublishMsg>(model);
                publishMsg.CreateTime = DateTime.Now;
                publishMsg.UserTypeInfoId = Current.UserTypeId;
                _publishMsgService.CreatePublishMsg(publishMsg);
            }
            catch (Exception e)
            {
                _logger.Debug("发布出错", e);
                throw e;
            }
            return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.OK, Message = "发布成功！", Url = Url.Action(nameof(HomeController.List),"Home") });
        }
        #endregion


    }
}