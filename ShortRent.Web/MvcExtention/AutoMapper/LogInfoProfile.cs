using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShortRent.Core.Domain;
using ShortRent.Web.Models;

namespace ShortRent.Web.MvcExtention.AutoMapper
{
    public class LogInfoProfile: Profile
    {
        public LogInfoProfile()
        {
            this.CreateMap<LogInfo, LogViewModelIndex>();
            this.CreateMap<LogInfo, LogDetailChange>();
            this.CreateMap<LogInfo, LogHumanModel>();
        }
    }
}