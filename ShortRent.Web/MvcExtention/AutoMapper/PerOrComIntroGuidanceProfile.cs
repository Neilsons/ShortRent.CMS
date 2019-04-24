using AutoMapper;
using ShortRent.Core.Domain;
using ShortRent.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.MvcExtention
{
    public class PerOrComIntroGuidanceProfile : Profile
    {
        public PerOrComIntroGuidanceProfile()
        {
            this.CreateMap<PerOrComIntroGuidance,PerOrComIntroGuidanceViewModel>();
            this.CreateMap<PerOrComIntroGuidanceViewModel,PerOrComIntroGuidance>();
            this.CreateMap<PerOrComIntroGuidanceViewModel,PerOrComIntroGuidanceHumanModel>().ForMember(c => c.Type, m => m.MapFrom(src => src.Type == true ? "招聘者" : "被招聘者"));
        }

    }
}