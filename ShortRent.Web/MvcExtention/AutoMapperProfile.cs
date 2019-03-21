using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ShortRent.Core.Domain;
using ShortRent.Web.Models;

namespace ShortRent.Web
{
    public class AutoMapperProfile:Profile
    {
       public AutoMapperProfile()
        {
            this.CreateMap<PersonViewModel,Person>();
        }
    }
}