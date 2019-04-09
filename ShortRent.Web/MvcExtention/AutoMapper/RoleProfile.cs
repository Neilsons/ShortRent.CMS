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
        }
    }
}