using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShortRent.Web.Models;
using ShortRent.Core.Domain;
namespace ShortRent.Web.MvcExtention.AutoMapper
{
    public class ManagerProfile:Profile
    {
        public ManagerProfile()
        {
            this.CreateMap<Manager, ManagerViewIndex>().ForMember(c=>c.IconInfo,m=>m.MapFrom(c=>c.ClassIcons+"|"+c.Color));
        }
    }
}