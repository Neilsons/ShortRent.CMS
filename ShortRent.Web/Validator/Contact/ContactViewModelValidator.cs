using FluentValidation;
using ShortRent.Web.Areas.ShortWeb.Models;
using ShortRent.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Validator.Contact
{
    public class ContactViewModelValidator : AbstractValidator<ContactViewModel>
    {
        public ContactViewModelValidator()
        {
            RuleFor(p => p.Name).NotNull().Length(1, 50);
            RuleFor(p => p.Email).NotNull().Length(1, 100).EmailAddress();
            RuleFor(p => p.Brief).NotNull().Length(1, 50);
            RuleFor(p => p.Content).NotNull();
        }
    }
}