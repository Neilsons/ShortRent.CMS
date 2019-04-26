using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using ShortRent.Core.Domain;
using ShortRent.Service;

namespace ShortRent.Web
{
    public class AuthenticationProvider : IAutnenticationProvider
    {
        #region Fields
        private readonly IPersonService _personService;
        #endregion
        #region Construction
        public AuthenticationProvider(IPersonService personService)
        {
            this._personService = personService;
        }
        #endregion
        public Person GetAutnenticationPerson()
        {
            HttpContext httpContext = HttpContext.Current;
            if (httpContext != null && httpContext.Request != null && httpContext.Request.IsAuthenticated && (httpContext.User.Identity is FormsIdentity))
            {
                FormsIdentity formsIdentity = (FormsIdentity)httpContext.User.Identity;
                string UserName = formsIdentity.Ticket.Name;
                string userData = formsIdentity.Ticket.UserData;
                if(!string.IsNullOrEmpty(userData))
                {
                    return _personService.GetPersons().Where(c => c.Type == true).SingleOrDefault(c=>c.ID==Convert.ToInt32(userData));
                }
            }
            return null;
        }
        //登入
        public void SignIn(Person model, bool readmeMe)
        {
            string userData = model.ID.ToString();
            var ticket = new FormsAuthenticationTicket(1, model.Name, DateTime.Now, DateTime.Now.AddDays(1), readmeMe, userData);
            //加密票据
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket) { HttpOnly=true};
            if(ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }
            //添加到当前相应中去
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        //登出
        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}