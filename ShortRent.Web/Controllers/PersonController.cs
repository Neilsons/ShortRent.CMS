using AutoMapper;
using ShortRent.Core.Domain;
using ShortRent.Core.Language;
using ShortRent.Core.Log;
using ShortRent.Service;
using ShortRent.Web.Models;
using ShortRent.WebCore.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShortRent.Web.Controllers
{
    public class PersonController : BaseController
    {
        #region Field 
        private readonly IPersonService _personService;
        //autoMapper
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfig;
        private readonly ILogger _logger;
        #endregion
        #region Construction
        public PersonController(IPersonService personService
            ,IMapper mapper,MapperConfiguration mapperConfiguration,ILogger logger)
        {
            this._personService = personService;
            this._mapper = mapper;
            this._mapperConfig = mapperConfiguration;
            this._logger = logger;
        }
        #endregion
        #region Method
        public ActionResult Index()
        {
            List<PersonViewModel> list = null;
            try
            {
                var persons = _personService.GetPersons();
                if (persons.Any())
                {
                    list = _mapper.Map<List<PersonViewModel>>(persons);
                }
                else
                {
                    list = new List<PersonViewModel>();
                }
            }
            catch(Exception e)
            {
                list = new List<PersonViewModel>();
                _logger.Debug("获得列表时出现错误",e);
                throw e;
            }
            return View(list.AsEnumerable());
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
        #endregion
    }
}