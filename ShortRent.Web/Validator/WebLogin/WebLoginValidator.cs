using FluentValidation;
using ShortRent.Web.Areas.ShortWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Validator
{
    public class WebLoginValidator : AbstractValidator<WebLoginModel>
    {
        public WebLoginValidator()
        {
            this.RuleFor(c => c.Name).NotNull().Length(1, 20);
            this.RuleFor(c => c.PassWord).NotNull().Length(1,120);
        }
    }
}