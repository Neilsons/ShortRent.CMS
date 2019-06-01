using AutoMapper;
using ShortRent.Core.Domain;
using ShortRent.Web.Areas.ShortWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web
{
    public class WebRegisterProfile : Profile
    {
        public WebRegisterProfile()
        {
            this.CreateMap<WebRegister, Person>();
            this.CreateMap<WebRegister, Company>().ForMember(c=>c.Name,m=>m.MapFrom(w=>w.CompanyName));
        }
    }
}