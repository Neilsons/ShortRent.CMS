using ShortRent.Core.Domain;
using ShortRent.Core.Infrastructure;
using ShortRent.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web
{
    public class WorkContext
    {
        #region
        public readonly IPersonService _personService;
        #endregion
        #region  Construction
        public WorkContext()
        {
            _personService = ServiceContainer.Resolve<IPersonService>();
        }
        #endregion
        public Person CurrentPerson
        {
            get
            {
                return new AuthenticationProvider(_personService).GetAutnenticationPerson();
            }
        }
        public PersonUserType CurrentWebPerson
        {
            get
            {
                return new AuthenticationProvider(_personService).GetAutnenticationPersonUserType();
            }
        }
    }
}