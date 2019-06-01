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
    public class CompanyController : BaseController
    {
        #region Fields
        private readonly ICompanyService _companyService;
        private readonly IUserTypeService _userTypeService;
        private readonly IPersonService _personService;
        private readonly IHistoryOperatorService _historyOperatorService;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfig;
        private readonly ILogger _logger;
        #endregion

        #region Contruction
        public CompanyController(ICompanyService companyService,
            IHistoryOperatorService historyOperatorService,
            IMapper mapper,
            MapperConfiguration mapperConfig,
            ILogger logger,
            IUserTypeService userTypeService,
            IPersonService personService)
        {
            this._companyService = companyService;
            this._userTypeService = userTypeService;
            this._personService = personService;
            this._historyOperatorService = historyOperatorService;
            this._mapper = mapper;
            this._mapperConfig = mapperConfig;
            this._logger = logger;
        }
        #endregion
        #region  Methods
        public ActionResult List()
        {
            ViewBag.Title = "公司管理";
            ViewBag.Content = "前台用户管理";
            return View();
        }
        public ActionResult Index(int pageSize, int pageNumber, string Name)
        {
            List<CompanyIndex> list = null;
            //返回的数据
            PagedListViewModel<CompanyIndex> pageList = new PagedListViewModel<CompanyIndex>();
            try
            {
                int total;
                var models = _companyService.GetPagedCompanys(pageSize, pageNumber, Name, out total);
                if (models.Any())
                {
                  
                    list = _mapper.Map<List<CompanyIndex>>(models);
                    foreach (var li in list)
                    {
                        //查找到那个table
                        int perId=_userTypeService.GetUserTypeById(li.ID).PerId;
                        //得到用户类型名称
                        li.UserTypeName = _personService.GetPerson(perId).Name;
                    }
                    pageList.Total = total;
                    pageList.Rows = list;
                }
            }
            catch(Exception e)
            {
                _logger.Debug("公司列表出错",e);
            }
            //获取所有的角色列表展示
            return Json(pageList, JsonRequestBehavior.AllowGet);

        }
        
        public ActionResult Audit(int id)
        {
            ViewBag.Title = "审核";
            ViewBag.Content = "公司审核";
            CompanyAudit companyAudit = new CompanyAudit();
            try
            {
                companyAudit = _mapper.Map<CompanyAudit>(_companyService.GetCompanyById(id));
            }
            catch(Exception e)
            {
                _logger.Debug("获取公司审核信息出错",e);
                return RedirectToAction("InternalServerError","System");
            }
            return View(companyAudit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Audit(CompanyAudit companyAudit)
        {
            try
            {
                Company company = _companyService.GetCompanyById(companyAudit.ID);
                string path = company.CompanyLicense;
                //映射已有的内容
                _mapper.Map(companyAudit,company);
                //更新公司内容
                company.CompanyLicense = path;
                _companyService.UpdateCompany(company);
                CompanyHumanModel humanModel = new CompanyHumanModel();
                humanModel = _mapper.Map<CompanyHumanModel>(company);
                HistoryOperator historyOperator = new HistoryOperator()
                {
                    CreateTime = DateTime.Now,
                    DetailDescirption = GetDescription<CompanyHumanModel>("审核公司信息", humanModel),
                    EntityModule = "公司管理",
                    Operates = "审核",
                    PersonId = GetCurrentPerson().ID,
                };
                _historyOperatorService.CreateHistoryOperator(historyOperator);
                return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.OK, Message = "审核成功", Url = Url.Action(nameof(CompanyController.List)) });

            }
            catch(Exception e)
            {
                _logger.Debug("审核公司信息出错",e);
                return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.InternalServerError, Url = Url.Action(nameof(SystemController.InternalServerError)) });
            }
        }
        #endregion
    }
}