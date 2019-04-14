using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using ShortRent.Core.Domain;
using ShortRent.Core.Log;
using ShortRent.Service;
using ShortRent.Web.Models;
using ShortRent.WebCore.MVC;
using ShortRent.Core;
using System.Net;

namespace ShortRent.Web.Controllers
{
    public class RoleController : BaseController
    {
        #region Fields
        private readonly IRoleService _roleService;
        private readonly IPermissionService _permissionService;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfig;
        private readonly ILogger _logger;
        #endregion
        #region Contruction
        public RoleController(IRoleService roleService,IMapper mapper,MapperConfiguration mapperConfig,ILogger logger,IPermissionService permissionService)
        {
            this._roleService = roleService;
            this._permissionService = permissionService;
            this._mapper = mapper;
            this._mapperConfig = mapperConfig;
            this._logger = logger;
        }
        #endregion
        // GET: Role
        /// <summary>
        /// 这个是role的打开视图
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            ViewBag.Title = "角色管理";
            ViewBag.Content = "角色列表";
            return View();
        }
        /// <summary>
        /// 通过json获取分页数据
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public JsonResult Index(int pageSize=5,int pageNumber=1,string roleName="")
        {
            List<RoleViewModelIndex> list = null;
            //返回的数据
            RolePageListViewModel pageList = new RolePageListViewModel();
            try
            {
                int total;
                var roles = _roleService.GetRoles(pageSize,pageNumber,roleName,out total);
                if (roles.Any())
                {
                    list = _mapper.Map<List<RoleViewModelIndex>>(roles);
                    pageList.Total = total;
                    pageList.Rows = list;
                }
            }
            catch(Exception e)
            {
                _logger.Debug(e.Message);
                throw new Exception(e.Message);
            }
            //获取所有的角色列表展示
            return Json(pageList,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Authorize(int id)
        {
            var groups = _permissionService.GetPermissions().GroupBy(c => c.Category);
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach(var group in groups)
            {
                var selectListGroup = new SelectListGroup { Name = group.Key };
                selectListItems.AddRange(group.Select(g => new SelectListItem {
                    Group = selectListGroup,
                    Selected = _roleService.GetPermissions(id).Any(rp => rp.ID == g.ID),
                    Text = g.Description,
                    Value=g.ID.ToString()
                }));
            }
            return View(new SelectList(selectListItems));
        }
        /// <summary>
        /// 角色创建显示页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "创建角色";
            ViewBag.Content = "创建角色表单";
            return View();
        }
        /// <summary>
        /// 角色创建表单提交
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoleViewModelIndex model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Role role = _mapper.Map<Role>(model);
                    role.CreateTime = DateTime.Now;
                    _roleService.CreateRole(role);
                    return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.OK, Message = "角色创建成功",Url=Url.Action(nameof(RoleController.List))});
                }
            }
            catch(Exception e)
            {
                _logger.Debug(e.Message);
                throw new Exception(e.Message);
            }
            return View(model);
        }
        /// <summary>
        /// 编辑页面创建
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "编辑角色";
            ViewBag.Content = "编辑角色表单";
            RoleViewModelIndex model = null;
            try
            {
                Role role = _roleService.GetRole(id);
                model = _mapper.Map<RoleViewModelIndex>(role);
                if(model==null)
                {
                    model = new RoleViewModelIndex();
                }
            }
            catch(Exception e)
            {
                _logger.Debug(e.Message);
                throw new Exception(e.Message);
            }
            return View("Create",model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RoleViewModelIndex model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Role role = _mapper.Map<Role>(model);
                    _roleService.UpdateRole(role);
                    return Json(new AjaxJson() { HttpCodeResult = (int)HttpStatusCode.OK, Message = "角色编辑成功", Url = Url.Action(nameof(RoleController.List)) });
                }
            }
            catch (Exception e)
            {
                _logger.Debug(e.Message);
                throw new Exception(e.Message);
            }
            return View("Create",model);
        }
    }
}