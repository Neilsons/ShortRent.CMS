using ShortRent.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShortRent.Web.Controllers
{
    public class PersonController : Controller
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
        #endregion
    }
}