using AutoMapper;
using ShortRent.Core.Domain;
using ShortRent.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.MvcExtention.AutoMapper
{
    public class BussinessProfile : Profile
    {
        public BussinessProfile()
        {
            this.CreateMap<BussinessIndex,Business>();
            this.CreateMap<Business,BussinessIndex>();
            this.CreateMap<Business, BussinessHuman>();
        }
    }
}