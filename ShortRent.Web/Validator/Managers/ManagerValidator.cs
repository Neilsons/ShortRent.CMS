using FluentValidation;
using ShortRent.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Validator.Managers
{
    public class ManagerValidator: AbstractValidator<ManagerCreteModel>
    {
        public ManagerValidator()
        {
            RuleFor(p => p.Name).NotNull().Length(1,50);
            RuleFor(p => p.ControllerName).Length(1, 50);
            RuleFor(p => p.ActionName).Length(1, 50);
            RuleFor(p => p.Activity).NotNull();
            RuleFor(p => p.ClassIcons).NotNull().Length(1, 50);
            RuleFor(p => p.Color).NotNull().Length(1, 50);
            RuleFor(p => p.Pid).NotNull();

        }
    }
}