using AutoMapper;
using ShortRent.Core.Domain;
using ShortRent.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.MvcExtention
{ 
    public class HistoryOperatorProfile : Profile
    {
        public HistoryOperatorProfile()
        {
            this.CreateMap<HistoryOperator, HistoryOperatorViewModel>().ForMember(c=>c.pName,m=>m.MapFrom(c=>c.Person.Name));
            this.CreateMap<HistoryOperator, HistoryOperatorDetail>().ForMember(c => c.pName, m => m.MapFrom(c => c.Person.Name));
        }
    }
}