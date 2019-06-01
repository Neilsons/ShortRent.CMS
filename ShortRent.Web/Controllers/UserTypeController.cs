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
    public class UserTypeController : BaseController
    {
        #region Fields
        private readonly IUserTypeService _userTypeService;
        private readonly IPersonService _personService;
        private readonly IHistoryOperatorService _historyOperatorService;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfig;
        private readonly ILogger _logger;
        #endregion

        #region Contruction
        public UserTypeController(IHistoryOperatorService historyOperatorService,
            IPersonService personService,
            IMapper mapper,
            MapperConfiguration mapperConfig,
            ILogger logger,
            IUserTypeService userTypeService)
        {
            this._userTypeService = userTypeService;
            this._personService = personService;
            this._historyOperatorService = historyOperatorService;
            this._mapper = mapper;
            this._mapperConfig = mapperConfig;
            this._logger = logger;
        }
        #endregion
        #region Methods
        public ActionResult List()
        {
            ViewBag.Title = "被招聘者管理";
            ViewBag.Content = "前台用户管理";
            return View();
        }
        public ActionResult Index(int pageSize, int pageNumber, string Name)
        {
            List<RecruiterByViewModel> list = null;
            //返回的数据
            PagedListViewModel<RecruiterByViewModel> pageList = new PagedListViewModel<RecruiterByViewModel>();
            try
            {
                int total;
                list = _userTypeService.GetRecruiterByViewModelList(pageSize, pageNumber, Name, out total);
                if (list.Any())
                {
                    pageList.Total = total;
                    pageList.Rows = list;
                }
            }
            catch (Exception e)
            {
                _logger.Debug("被招聘者列表出错", e);
            }
            //获取所有的被招聘者列表展示
            return Json(pageList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Audit(int id)
        {
            ViewBag.Title = "审核";
            ViewBag.Content = "被招聘者管理";
            UserTypeAudit userTypeAudit = new UserTypeAudit();
            try
            {
                userTypeAudit = _userTypeService.GetUserAudit(id);
            }
            catch (Exception e)
            {
                _logger.Debug("获取被招聘者审核信息出错", e);
                return RedirectToAction("InternalServerError", "System");
            }
            return View(userTypeAudit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Audit(UserTypeAudit userTypeAudit)
        {
            try
            {
                //获得人的信息
                Person person = _personService.GetPerson(userTypeAudit.ID);
                _mapper.Map(userTypeAudit,person);
                //更新人的基本信息
                _personService.UpdatePerson(person);
                //获得USerType
                UserType userType = _userTypeService.GetUserTypeById(userTypeAudit.UserTypeId);
                //更新UserType
                userType.TypeUser = userTypeAudit.TypeUser;
                userType.TypeMessage = userTypeAudit.TypeMessage;
                _userTypeService.UpdateUserType(userType);
                UserTypeAuditHumanModel humanModel = new UserTypeAuditHumanModel();
                humanModel = _mapper.Map<UserTypeAuditHumanModel>(userTypeAudit);
                HistoryOperator historyOperator = new HistoryOperator()
                {
                    CreateTime = DateTime.Now,
                    DetailDescirption = GetDescription<UserTypeAuditHumanModel>("审核被招聘者信息", humanModel),
                    EntityModule = "被招聘者管理",
                    Operates = "审核",
                    PersonId = GetCurrentPerson().ID,
                };
                _historyOperatorService.CreateHistoryOperator(historyOperator);
                return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.OK, Message = "审核成功", Url = Url.Action(nameof(UserTypeController.List)) });

            }
            catch (Exception e)
            {
                _logger.Debug("审核被招聘者信息出错", e);
                return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.InternalServerError, Url = Url.Action(nameof(SystemController.InternalServerError)) });
            }
        }
        public ActionResult ReduitList()
        {
            ViewBag.Title = "招聘者管理";
            ViewBag.Content = "前台用户管理";
            return View();
        }
        public ActionResult ReduitIndex(int pageSize, int pageNumber, string Name)
        {
            List<RecruiterViewModel> list = null;
            //返回的数据
            PagedListViewModel<RecruiterViewModel> pageList = new PagedListViewModel<RecruiterViewModel>();
            try
            {
                int total;
                list = _userTypeService.GetRecruiterViewModelList(pageSize, pageNumber, Name, out total);
                if (list.Any())
                {
                    pageList.Total = total;
                    pageList.Rows = list;
                }
            }
            catch (Exception e)
            {
                _logger.Debug("招聘者列表出错", e);
            }
            //获取招聘者列表展示
            return Json(pageList, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}