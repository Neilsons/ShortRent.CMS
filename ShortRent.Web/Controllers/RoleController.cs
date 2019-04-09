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
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfig;
        private readonly ILogger _logger;
        #endregion
        #region Contruction
        public RoleController(IRoleService roleService,IMapper mapper,MapperConfiguration mapperConfig,ILogger logger)
        {
            this._roleService = roleService;
            this._mapper = mapper;
            this._mapperConfig = mapperConfig;
            this._logger = logger;
        }
        #endregion
        // GET: Role
        public ActionResult Index()
        {
            List<RoleViewModelIndex> list = null;
            try
            {
                var roles = _roleService.GetRoles();
                if (roles.Any())
                {
                    list = _mapper.Map<List<RoleViewModelIndex>>(roles);
                }
                else
                {
                    list = new List<RoleViewModelIndex>();
                }
            }
            catch(Exception e)
            {
                list = new List<RoleViewModelIndex>();
                _logger.Debug(e.Message);
                throw new Exception(e.Message);
            }
            //获取所有的角色列表展示
            return View(list.AsEnumerable());
        }
    }
}