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
    public class CompanyPerTagsController : BaseController
    {
        #region Fields
        private readonly ICompanyPerTagsService _companyPerTagsService;
        private readonly IHistoryOperatorService _historyOperatorService;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfig;
        private readonly ILogger _logger;
        #endregion

        #region Contruction
        public CompanyPerTagsController(ICompanyPerTagsService companyPerTagsService, IHistoryOperatorService historyOperatorService, IMapper mapper, MapperConfiguration mapperConfig, ILogger logger)
        {
            this._companyPerTagsService = companyPerTagsService;
            this._historyOperatorService = historyOperatorService;
            this._mapper = mapper;
            this._mapperConfig = mapperConfig;
            this._logger = logger;
        }
        #endregion
        #region Methods
        public ActionResult List()
        {
            ViewBag.Title = "标签管理";
            ViewBag.Content = "标签列表";
            return View();
        }
        public ActionResult Index(int pageSize,int pageNumber,string tagName)
        {
            List<PublishTagsViewModel> list = null;
            //返回的数据
            PagedListViewModel<PublishTagsViewModel> pageList = new PagedListViewModel<PublishTagsViewModel>();
            try
            {
                int total;
                var models = _companyPerTagsService.GetPagedCompanyPerTags(pageSize, pageNumber, tagName, out total);
                if (models.Any())
                {
                    list = _mapper.Map<List<PublishTagsViewModel>>(models);
                    pageList.Total = total;
                    pageList.Rows = list;
                }
            }
            catch (Exception e)
            {
                _logger.Debug("获取标签分页数据列表出错", e);
                throw e;
            }
            //获取所有的角色列表展示
            return Json(pageList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.Title = "标签管理";
            ViewBag.Content = "标签列表";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PublishTagsViewModel model)
        {
            try
            {
                var companyPerTags = _mapper.Map<CompanyPerTag>(model);
                companyPerTags.CreateTime = DateTime.Now;
                companyPerTags.IsPublish = true;
                _companyPerTagsService.Create(companyPerTags);
                //创建人性化的类
                PublishTagsHumanModel human = _mapper.Map<PublishTagsHumanModel>(companyPerTags);
                //创建完之后更新到历史记录中去
                HistoryOperator history = new HistoryOperator()
                {
                    CreateTime = DateTime.Now,
                    EntityModule = "标签管理",
                    DetailDescirption = GetDescription<PublishTagsHumanModel>("创建了一个标签，详情", human),
                    Operates = "创建",
                    PersonId = GetCurrentPerson().ID,
                };
                _historyOperatorService.CreateHistoryOperator(history);
            }
            catch (Exception e)
            {
                _logger.Debug("创建标签提交出错！", e);
                throw e;
            }
            return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.OK, Message = "创建标签成功！", Url = Url.Action(nameof(CompanyPerTagsController.List)) });
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "编辑标签";
            ViewBag.Content = "编辑标签表单";
            PublishTagsViewModel model = null;
            try
            {
                CompanyPerTag icons = _companyPerTagsService.GetCompanyPerTags(id);
                model = _mapper.Map<PublishTagsViewModel>(icons);
                if (model == null)
                {
                    model = new PublishTagsViewModel();
                }
            }
            catch (Exception e)
            {
                _logger.Debug("编辑标签显示界面", e); ;
                throw e;
            }
            return View("Create", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PublishTagsViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CompanyPerTag newModel = _mapper.Map<CompanyPerTag>(model);
                    //获取之前的用户
                    CompanyPerTag oldModel = _companyPerTagsService.GetCompanyPerTags(model.ID);
                    _companyPerTagsService.Update(newModel);
                    //得到转换后的人性化模型
                    PublishTagsHumanModel hisModel = _mapper.Map<PublishTagsHumanModel>(newModel);
                    PublishTagsHumanModel oldHum = _mapper.Map<PublishTagsHumanModel>(oldModel);
                    HistoryOperator history = new HistoryOperator()
                    {
                        CreateTime = DateTime.Now,
                        DetailDescirption = GetDescription<PublishTagsHumanModel>("编辑了一个标签，详情", hisModel, oldHum),
                        EntityModule = "标签管理",
                        Operates = "编辑",
                        PersonId = GetCurrentPerson().ID,
                    };
                    _historyOperatorService.CreateHistoryOperator(history);
                    return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.OK, Message = "标签编辑成功", Url = Url.Action(nameof(CompanyPerTagsController.List)) });
                }
            }
            catch (Exception e)
            {
                _logger.Debug("编辑标签提交出错", e);
                throw e;
            }
            return View("Create", model);
        }
        public ActionResult Delete(int id)
        {
            try
            {
                //得到图标信息
                var companyPerTag = _companyPerTagsService.GetCompanyPerTags(id);
                _companyPerTagsService.Delete(companyPerTag);
                //删除一个图标之后就需要往历史记录中插入一条历史
                //得到要展示的那个实体
                PublishTagsHumanModel Human = _mapper.Map<PublishTagsHumanModel>(companyPerTag);
                HistoryOperator historyOperator = new HistoryOperator()
                {
                    CreateTime = DateTime.Now,
                    DetailDescirption = GetDescription<PublishTagsHumanModel>("删除了一个标签，详情", Human),
                    EntityModule = "标签管理",
                    Operates = "删除",
                    PersonId = GetCurrentPerson().ID,
                };
                //插入记录
                _historyOperatorService.CreateHistoryOperator(historyOperator);
            }
            catch (Exception e)
            {
                _logger.Debug("删除标签出错", e);
                throw e;
            }
            return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.OK, Message = "删除标签成功" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}