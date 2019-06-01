using FluentValidation;
using ShortRent.Web.Areas.ShortWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Validator.WebRegisters
{
    public class WebRegisterValidator : AbstractValidator<WebRegister>
    {
        public WebRegisterValidator()
        {
            RuleFor(p => p.Name).NotNull().Length(1, 20);
            RuleFor(p => p.Birthday).NotNull();
            RuleFor(p => p.Type).NotNull();
            RuleFor(p => p.PerImage).NotNull().Length(1,200);
            RuleFor(p => p.PassWord).NotNull().Length(1, 120);
            RuleFor(p => p.IdCard).NotNull().Length(1, 18);
            RuleFor(p => p.Position).Length(1, 50);
            RuleFor(p => p.PersonDetail).Length(1, 500);
            RuleFor(p => p.Qq).Length(1, 50);
            RuleFor(p => p.WeChat).Length(1, 50);
            RuleFor(p => p.IdCardFront).Length(1, 200);
            RuleFor(p => p.IdCardBack).Length(1, 200);
            RuleFor(p => p.CompanyName).NotNull().Length(1, 50);
            RuleFor(p => p.CompanyImg).Length(1, 500);
            RuleFor(p => p.CompanyLicense).Length(1, 500);
            RuleFor(p => p.EmployeesCount).NotNull();
            RuleFor(p => p.EstablishTime).NotNull();
            RuleFor(p => p.Address).NotNull();
            RuleFor(p => p.Confirm).NotNull();
        }
    }
}