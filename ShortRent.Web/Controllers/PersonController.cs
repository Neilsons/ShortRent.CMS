using AutoMapper;
using ShortRent.Core.Domain;
using ShortRent.Service;
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
        #endregion
        #region Construction
        public PersonController(IPersonService personService
            ,IMapper mapper,MapperConfiguration mapperConfiguration)
        {
           this._personService = personService;
            this._mapper = mapper;
            this._mapperConfig = mapperConfiguration;
        }
        #endregion
        #region Method
        public ActionResult Index()
        {
            return View(_personService.GetPersons());
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
            return View();
        }
        #endregion
    }
}