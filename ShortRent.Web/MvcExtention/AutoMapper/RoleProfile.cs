using AutoMapper;
using ShortRent.Core.Domain;
using ShortRent.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web
{
    public class RoleProfile:Profile
    {
        public RoleProfile()
        {
            this.CreateMap<Role, RoleViewModelIndex>();
            this.CreateMap<RoleViewModelIndex, Role>();
            this.CreateMap<RoleViewModelIndex,RoleHumanModel>().ForMember(c=>c.Type,m=>m.MapFrom(src=>src.Type==true?"后台":"前台"));
        }
    }
}