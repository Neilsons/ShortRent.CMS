using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShortRent.Web.Models;

namespace ShortRent.Web
{
    public class PersonAdminValidator:AbstractValidator<PersonAdminEditModel>
    {
        public PersonAdminValidator()
        {
            this.RuleFor(p => p.Name).Length(1,20).NotNull();
            this.RuleFor(p => p.PerImage).NotNull();
            this.RuleFor(p => p.Qq).Length(1, 50).NotNull();
            this.RuleFor(p => p.WeChat).Length(1, 50).NotNull();
            this.RuleFor(p => p.PersonDetail).Length(1,500).NotNull();
            this.RuleFor(p=>p.Position).Length(1,50).NotNull();
            this.RuleFor(p => p.PassWord).Length(1, 120).NotNull();
        }
    }
}