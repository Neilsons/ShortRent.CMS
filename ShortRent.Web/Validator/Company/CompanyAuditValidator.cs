using FluentValidation;
using ShortRent.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Validator
{
    public class CompanyAuditValidator : AbstractValidator<CompanyAudit>
    {
        public CompanyAuditValidator()
        {
            RuleFor(p => p.Name).NotNull().Length(1, 50);
            RuleFor(p => p.Score).NotNull();
            RuleFor(p => p.EstablishTime).NotNull();
            RuleFor(p=>p.CompanyStatus).NotNull();
            RuleFor(p => p.CompanyMessage).NotNull().Length(1,100);
        }
    }
}