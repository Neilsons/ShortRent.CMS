using AutoMapper;
using ShortRent.Core.Domain;
using ShortRent.Web.Areas.ShortWeb.Models;
using ShortRent.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.MvcExtention.AutoMapper
{
    public class PublishMsgProfile : Profile
    {
        public PublishMsgProfile()
        {
            this.CreateMap<PublishCreateModel, PublishMsg>();
            this.CreateMap<PublishMsg, PublishMsgWatch>();
        }
    }
}