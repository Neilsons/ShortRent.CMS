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
        #endregion
        #region Construction
        public PersonController(IPersonService personService)
        {
           this._personService = personService;
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
        public ActionResult Create(Person model)
        {
            if(ModelState.IsValid)
            {
                _personService.CreatePerson(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        #endregion
    }
}