using AutoMapper;
using ShortRent.Core.Domain;
using ShortRent.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.MvcExtention
{
    public class CompanyProfile:Profile
    {
        public CompanyProfile()
        {
            this.CreateMap<Company, CompanyIndex>();
            this.CreateMap<Company, CompanyAudit>();
            this.CreateMap<CompanyAudit, Company>();
            this.CreateMap<Company, CompanyHumanModel>().ForMember(c=>c.CompanyStatus,m=>m.MapFrom(c=>c.CompanyStatus==1?"审核通过":"审核未通过"));
        }
    }
}