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
            this.CreateMap<PersonLoginModel, Person>();
            this.CreateMap<Person, PersonAdminEditModel>();
            this.CreateMap<PersonAdminEditModel, Person>();
            this.CreateMap<Person, PersonAdminIndexModel>();
            this.CreateMap<PersonAdminEditModel, Person>();
            this.CreateMap<Person, PersonAdminUpdate>();
            this.CreateMap<PersonAdminUpdate,Person>();
            this.CreateMap<PersonAdminEditModel, PersonAdminHumanEditModel>();
            this.CreateMap<Person, PersonAdminHumanEditModel>();
        }
    }
}