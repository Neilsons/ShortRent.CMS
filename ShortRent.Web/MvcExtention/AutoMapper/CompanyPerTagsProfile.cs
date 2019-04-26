using AutoMapper;
using ShortRent.Core.Domain;
using ShortRent.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.MvcExtention
{
    public class CompanyPerTagsProfile : Profile
    {
        public CompanyPerTagsProfile()
        {
            this.CreateMap<CompanyPerTag, PublishTagsViewModel>();
            this.CreateMap<PublishTagsViewModel, CompanyPerTag>();
            this.CreateMap<CompanyPerTag, PublishTagsHumanModel>();
        }
    }
}