using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using ShortRent.Core.Log;
using ShortRent.Service;
using ShortRent.Web.Areas.ShortWeb.Models;
using ShortRent.Web.Models;
using ShortRent.WebCore.MVC;

namespace ShortRent.Web.Controllers
{
    public class ContactController : BaseController
    {
        #region Fields
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfig;
        private readonly ILogger _logger;
        #endregion

        #region Contruction
        public ContactController(
            IContactService contactService,
            IMapper mapper,
            MapperConfiguration mapperConfig,
            ILogger logger)
        {
            this._contactService = contactService;
            this._mapper = mapper;
            this._mapperConfig = mapperConfig;
            this._logger = logger;
        }
        #endregion
        #region Methods
        public ActionResult List()
        {
            ViewBag.Title = "联系内容管理";
            ViewBag.Content = "信息管理";
            return View();
        }
        public ActionResult Index(int pageSize, int pageNumber, DateTime? startTime, DateTime? endTime)
        {
            List<ContactViewModel> list = null;
            PagedListViewModel<ContactViewModel> pageList = new PagedListViewModel<ContactViewModel>();
            try
            {
                int total;
                var contacts = _contactService.GetContacts(pageSize,pageNumber, startTime, endTime, out total);
                if (contacts.Any())
                {
                    list = _mapper.Map<List<ContactViewModel>>(contacts);
                    pageList.Total = total;
                    pageList.Rows = list;
                }
            }
            catch (Exception e)
            {
                _logger.Debug("得到分页数据出错，控制器下", e);
                throw e;
            }
            return Json(pageList, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}