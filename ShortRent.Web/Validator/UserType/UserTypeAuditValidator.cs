using FluentValidation;
using ShortRent.Core.Domain;
using ShortRent.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Validator
{
    public class UserTypeAuditValidator : AbstractValidator<UserTypeAudit>
    {
        public UserTypeAuditValidator()
        {
            this.RuleFor(c => c.Name).NotNull().Length(1,20);
            this.RuleFor(c => c.Birthday).NotNull();
            this.RuleFor(c => c.CreditScore).NotNull();
            this.RuleFor(c => c.IdCard).NotNull().Length(1, 18);
            this.RuleFor(c => c.IdCardFront).NotNull().Length(1, 200);
            this.RuleFor(c => c.IdCardBack).NotNull().Length(1, 200);
            this.RuleFor(c => c.TypeUser).NotNull();
            this.RuleFor(c => c.TypeMessage).NotNull();
        }
    }
}