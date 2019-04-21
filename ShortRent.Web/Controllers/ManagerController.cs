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
using ShortRent.Core.Domain;

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
                //下拉列表框值
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
                    var manager = _mapper.Map<Manager>(model);
                    //判断是否为根节点
                    if(manager.Pid==0)
                    {
                        manager.Pid = null; 
                    }
                    manager.CreateTime = DateTime.Now;
                    _managerService.CreateManager(manager);
                    //将创建的新的保存到历史操作中去
                    //拿到人性化的类
                    ManagerHumanModel human = _mapper.Map<ManagerHumanModel>(manager);
                    //获取父级菜单
                    string PName = _managerService.GetManager(manager.ID).Name;
                    human.PidName = PName;
                    //创建操作历史对象
                    HistoryOperator history = new HistoryOperator()
                    {
                        CreateTime = DateTime.Now,
                        DetailDescirption = GetDescription<ManagerHumanModel>("创建了一个菜单，详情", human),
                        EntityModule = "系统管理",
                        Operates = "菜单创建",
                        PersonId = 1
                    };
                    _historyOperatorService.CreateHistoryOperator(history);
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
            return Json(new AjaxJson() { HttpCodeResult = (int)System.Net.HttpStatusCode.OK, Message = "创建菜单成功!", Url = Url.Action(nameof(ManagerController.List)) });
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Title = "菜单列表";
            ViewBag.Content = "菜单编辑";
            ManagerCreteModel manager = new ManagerCreteModel();
            try
            {
                var model = _managerService.GetManager(id);
                manager = _mapper.Map<ManagerCreteModel>(model);
                var list = _managerService.GetTreeViewManagers();
                List<ManagerBread> breads = _mapper.Map<List<ManagerBread>>(list);
                //下拉列表框值
                ViewBag.DropData = GetManagerBreads(breads);
            }
            catch (Exception e)
            {
                _logger.Debug("编辑菜单表单错误", e);
                throw e;
            }
            return View("Create",manager);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ManagerCreteModel creteModel)
        {
            try
            {
                if(creteModel.Pid==0)
                {
                    creteModel.Pid = null;
                }
                if (ModelState.IsValid)
                {
                    Manager manager = _mapper.Map<Manager>(creteModel);
                    //先获取之前的那个模型
                    Manager oldManager = _managerService.GetManager(creteModel.ID);
                    manager.CreateTime = oldManager.CreateTime;
                    string oldPName=_managerService.GetManager(oldManager.Pid).Name;
                    string pName = _managerService.GetManager(manager.Pid).Name;
                    //更新现有的模型
                    _managerService.UpdateManager(manager);
                    //将新旧的模型转化为humanModel
                    ManagerHumanModel human = _mapper.Map<ManagerHumanModel>(manager);
                    ManagerHumanModel oldHuman = _mapper.Map<ManagerHumanModel>(oldManager);
                    human.PidName = pName;
                    human.PidName = oldPName;
                    HistoryOperator historyOperator = new HistoryOperator()
                    {
                        CreateTime = DateTime.Now,
                        DetailDescirption = GetDescription<ManagerHumanModel>("编辑菜单，详情", human, oldHuman),
                        EntityModule = "系统管理",
                        Operates = "菜单编辑",
                        PersonId = 1
                    };
                    _historyOperatorService.CreateHistoryOperator(historyOperator);
                }
                else
                {
                    return View("Create", creteModel);
                }
            }
            catch (Exception e)
            {
                _logger.Debug("编辑提交时出错！", e);
                throw e;
            }
            return Json(new AjaxJson() { HttpCodeResult = (int)System.Net.HttpStatusCode.OK, Message = "编辑模型成功", Url = Url.Action(nameof(ManagerController.List)) });
        }
        /// <summary>
        /// 得到树形数据
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private List<ManagerBread> GetManagerBreads(List<ManagerBread> nodes)
        {
            List<ManagerBread> managerBreads = new List<ManagerBread>();
            foreach (ManagerBread node in nodes)
            {
                ManagerBread copy = new ManagerBread();
                copy.ClassIcons = node.ClassIcons;
                copy.Color = node.Color;
                copy.Name = node.Name;
                copy.ID = node.ID;
                copy.Pid = node.ID;
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