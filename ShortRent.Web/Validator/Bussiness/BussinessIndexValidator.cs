using FluentValidation;
using ShortRent.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Validator.Bussiness
{
    public class BussinessIndexValidator : AbstractValidator<BussinessIndex>
    {
        public BussinessIndexValidator()
        {
            RuleFor(p => p.Name).NotNull().Length(1, 50);
        }
    }
}