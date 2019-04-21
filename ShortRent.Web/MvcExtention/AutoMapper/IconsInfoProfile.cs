using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShortRent.Web.Models;
using ShortRent.Core.Domain;

namespace ShortRent.Web.MvcExtention
{
    public class IconsInfoProfile:Profile
    {
        public IconsInfoProfile()
        {
            this.CreateMap<IconsInfo,IconViewModel>();
            this.CreateMap<IconsInfo, IconInfoShowModel>().ForMember(c => c.IconInfo, m => m.MapFrom(c => c.prefix + " " + c.Content));
            this.CreateMap<IconViewModel, IconsInfo>();
            this.CreateMap<IconsInfo, IconHuman>();

        }
         
    }
}