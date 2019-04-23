using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using ShortRent.Web.Models;

namespace ShortRent.Web.Validator.Persons
{
    public class EditPersonPassWordValidatorbstractValidator: AbstractValidator<PassWordEditModel>
    {
        public EditPersonPassWordValidatorbstractValidator()
        {
            this.RuleFor(p => p.OldPassWord).Length(1, 120).NotNull();
            this.RuleFor(p => p.PassWord).Length(1, 120).NotNull();
            this.RuleFor(p => p.ConfirmPassWord).Length(1, 120).NotNull();
        }

    }
}