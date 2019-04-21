using ShortRent.Core.Domain;
using ShortRent.Core.Infrastructure;
using ShortRent.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web
{
    public static class WorkContext
    {
        #region
        public static readonly IPersonService _personService;
        #endregion
        #region  Construction
        static WorkContext()
        {
            _personService = ServiceContainer.Resolve<IPersonService>();
        }
        #endregion
        public static Person CurrentPerson
        {
            get
            {
                return new AuthenticationProvider(_personService).GetAutnenticationPerson();
            }
        }
    }
}