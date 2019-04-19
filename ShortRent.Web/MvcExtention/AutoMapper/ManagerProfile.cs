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
            this.CreateMap<Manager, ManagerBread>();
            this.CreateMap<ManagerCreteModel, Manager>();
            this.CreateMap<ManagerCreteModel, Manager>();
            this.CreateMap<Manager, ManagerHumanModel>().ForMember(c => c.Activity, m => m.MapFrom(c => c.Activity == true ? "已启用" : "已禁用"));
        }
    }
}