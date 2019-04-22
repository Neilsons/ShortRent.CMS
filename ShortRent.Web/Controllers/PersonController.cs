using AutoMapper;
using ShortRent.Core;
using ShortRent.Core.Domain;
using ShortRent.Core.Language;
using ShortRent.Core.Log;
using ShortRent.Service;
using ShortRent.Web.Models;
using ShortRent.WebCore.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ShortRent.Web.Controllers
{
    public class PersonController : BaseController
    {
        #region Field 
        private readonly IPersonService _personService;
        private readonly IHistoryOperatorService _historyOperatorService;
        private readonly IAutnenticationProvider _authenticationProvider;
        //autoMapper
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfig;
        private readonly ILogger _logger;
        #endregion
        #region Construction
        public PersonController(IPersonService personService
            ,IMapper mapper,
            MapperConfiguration mapperConfiguration,ILogger logger
            ,IHistoryOperatorService historyOperator
            ,IAutnenticationProvider autnentication)
        {
            this._personService = personService;
            this._historyOperatorService = historyOperator;
            this._authenticationProvider = autnentication;
            this._mapper = mapper;
            this._mapperConfig = mapperConfiguration;
            this._logger = logger;
        }
        #endregion
        #region Method
        public ActionResult List()
        {
            ViewBag.Title = "后台用户管理";
            ViewBag.Content = "列表";
            return View();
        }
        public ActionResult Index(int? pageSize,int? pageNumber,string AdminName)
        {
            List<PersonAdminIndexModel> list = null;
            PagedListViewModel<PersonAdminIndexModel> paged = new PagedListViewModel<PersonAdminIndexModel>();
            try
            {
                int total;
                var persons = _personService.GetTypePerson(pageSize ?? 0, pageNumber ?? 0,AdminName, 1, out total);
                if(persons.Any())
                {
                    list = _mapper.Map<List<PersonAdminIndexModel>>(persons);
                }
                else
                {
                    list = new List<PersonAdminIndexModel>();
                }
                paged.Rows = list;
                paged.Total = total;
            }
            catch(Exception e)
            {
                list = new List<PersonAdminIndexModel>();
                _logger.Debug("获得列表时出现错误",e);
                throw e;
            }
            return Json(paged,JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetJson()
        {
            return Json(_personService.GetPersons(),JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Person model)
        {
            if(ModelState.IsValid)
            {
                _personService.CreatePerson(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        public ActionResult Test()
        {
            ViewBag.Title = "测试成功";
            ViewBag.Data = RouteData.DataTokens["language"].ToString();
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(PersonLoginModel model,string returnUrl)
        {
            try
            {
                //获得所有的用户
                var person = _personService.GetPersons().Where(c=>c.Type==true).SingleOrDefault(c => c.Name == model.Name && c.PassWord == model.PassWord);
                if(person!=null)
                {
                    var singnPer = person;
                    //身份登陆成功
                    _authenticationProvider.SignIn(singnPer, model.ReadMe);
                    HistoryOperator history = new HistoryOperator()
                    {
                        CreateTime = DateTime.Now,
                        DetailDescirption = model.Name + "登陆了系统",
                        EntityModule = "用户登陆",
                        Operates = "登陆",
                        PersonId = person.ID
                    };
                    _historyOperatorService.CreateHistoryOperator(history);
                    return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.OK, Message = "登陆成功！",Url=Url.Action(nameof(PersonController.Home))});
                }
            }
            catch(Exception e)
            {
                _logger.Debug("登陆出现错误！",e);
                throw e;
            }
            return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.NotFound, Message = "用户名或密码错误", Url =Url.Action(nameof(PersonController.Login)) });
        }
        public ActionResult Home()
        {
            ViewBag.Title = "后台首页";
            return View();
        }
        public ActionResult SignOut()
        {
            _authenticationProvider.SignOut();
            throw new HttpException((int)HttpStatusCode.Unauthorized,"");
        }
        public ActionResult PersonalData()
        {
            ViewBag.Title = "个人资料";
            ViewBag.Content = "详情";
            return View();
        }
        public ActionResult PersonAdminDetail()
        {
            ViewBag.Title = "个人资料编辑";
            ViewBag.Content = "编辑";
            PersonAdminEditModel admin = null;
            try
            {
                //得到当前人的信息
                admin = _mapper.Map<PersonAdminEditModel>(WorkContext.CurrentPerson);
            }
            catch(Exception e)
            {
                _logger.Debug("后台人员修改个人资料出错");
                throw e;
            }
            return View(admin);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PersonAdminDetail(PersonAdminEditModel model, HttpPostedFileBase headPhoto)
        {
            try
            {

            }
            catch(Exception e)
            {
                _logger.Debug("修改详情出错",e);
                throw e;
            }
            return View();
        }
        #endregion
    }
}