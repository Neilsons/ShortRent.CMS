using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using ShortRent.Core.Log;
using ShortRent.Service;
using ShortRent.Web.Models;
using ShortRent.WebCore.MVC;
using ShortRent.Core;

namespace ShortRent.Web.Controllers
{
    public class ManagerController : BaseController
    {
        #region Fields
        private readonly IManagerService _managerService;
        private readonly IHistoryOperatorService _historyOperatorService;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfig;
        private readonly ILogger _logger;
        #endregion

        #region Construction
        public ManagerController(IManagerService managerService,IHistoryOperatorService historyOperatorService, IMapper mapper, MapperConfiguration mapperConfig, ILogger logger)
        {
            this._managerService = managerService;
            this._historyOperatorService = historyOperatorService;
            this._mapper = mapper;
            this._mapperConfig = mapperConfig;
            this._logger = logger;
        }
        #endregion
        public ActionResult List()
        {
            ViewBag.Title = "系统管理";
            ViewBag.Content = "菜单列表";
            return View();
        }
        public ActionResult Index(string order)
        {
            List<ManagerViewIndex> managers = null;
            try
            {
                var list=_managerService.GetManagers();
                if(list.Any())
                {
                    managers = _mapper.Map <List<ManagerViewIndex>>(list);
                }
            }
            catch(Exception e)
            {
                _logger.Debug("菜单列表展示", e);
                throw e;
            }
            return Json(managers,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {
            ViewBag.Title = "菜单列表";
            ViewBag.Content = "菜单添加";
            try
            {
                var list = _managerService.GetTreeViewManagers();
                List<ManagerBread> breads = _mapper.Map<List<ManagerBread>>(list);
                ViewBag.DropData = GetManagerBreads(breads);
            }
            catch(Exception e)
            {
                _logger.Debug("创建菜单表单错误",e);
                throw e;
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ManagerCreteModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    
                }
                else
                {
                    return View(model);
                }
            }
            catch(Exception e)
            {
                _logger.Debug("创建菜单提交出错",e);
                throw e;
            }
            return Json(new AjaxJson() { HttpCodeResult = (int)System.Net.HttpStatusCode.OK, Message = "创建菜单成功!", Url = Url.Action("Index") });
        }
        /// <summary>
        /// 得到树形数据
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private List<ManagerBread> GetManagerBreads(List<ManagerBread> nodes)
        {
            List<ManagerBread> managerBreads = null;
            foreach (ManagerBread node in nodes)
            {
                ManagerBread copy = new ManagerBread();
                copy.ClassIcons = node.ClassIcons;
                copy.Color = node.Color;

                copy.ControllerName = node.ControllerName;
                copy.ActionName = node.ActionName;
                copy.HasActiveChildren = node.Childrens.Any(child => child.Activity || (child.HasActiveChildren ?? false));
                copy.Activity = node.Childrens.Any(child => child.Activity && !(child.ControllerName == null && child.ActionName == null));
                copy.Childrens = GetManagerBreads(node.Childrens);
                managerBreads.Add(copy);
            }
            return managerBreads;
        }
    }
}