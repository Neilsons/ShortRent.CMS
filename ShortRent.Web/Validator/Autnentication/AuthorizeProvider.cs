using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShortRent.Core.Domain;

namespace ShortRent.Web
{
    public class AuthorizeProvider : IAuthroizeProvider
    {
        #region Fields

        #endregion
        #region Construction
        public AuthorizeProvider()
        {

        }
        #endregion
        public bool Authorize(string permissionName, Person person)
        {
            return true;
        }

        public bool Authorize(string permissionName)
        {
            return true;
        }
    }
}