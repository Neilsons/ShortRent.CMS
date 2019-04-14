using FluentValidation;
using ShortRent.Web.Models;
using ShortRent.Resource.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Validator.Roles
{
    public class RoleViewModelIndexValidator:AbstractValidator<RoleViewModelIndex>
    {
        public RoleViewModelIndexValidator()
        {
            this.RuleFor(c=>c.Name).NotNull().Length(1,100).WithLocalizedMessage(()=>Resources.Scope);
            this.RuleFor(c => c.Type).NotNull();
        }
    }
}