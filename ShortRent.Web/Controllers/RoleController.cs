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
    }
}