using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ShortRent.Core.Domain;
using ShortRent.Web.Models;

namespace ShortRent.Web
{
    public class PersonProfile: Profile
    {
       public PersonProfile()
        {
            this.CreateMap<PersonViewModel,Person>();
            this.CreateMap<Person, PersonViewModel>();
        }
    }
}