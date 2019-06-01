using FluentValidation;
using ShortRent.Web.Areas.ShortWeb.Models;
using ShortRent.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Validator.PublishMsg
{
    public class PublishCreateModelValidator : AbstractValidator<PublishCreateModel>
    {
        public PublishCreateModelValidator()
        {
            this.RuleFor(p => p.Phone).Length(1, 20).NotNull();
            this.RuleFor(p => p.Address).Length(1, 200).NotNull();
            this.RuleFor(p => p.Email).Length(1, 50).NotNull().EmailAddress();
            this.RuleFor(p => p.Currency).Length(1,20).NotNull();
            this.RuleFor(p => p.StartSection).NotNull();
            this.RuleFor(p => p.EndSection).NotNull();
            this.RuleFor(p => p.Decription).Length(1, 200).NotNull();
            this.RuleFor(p => p.Detail).NotNull();
            this.RuleFor(p => p.UserTypeInfoId).NotNull();
            this.RuleFor(p => p.BusinessTypeId).NotNull();
        }

    }
}