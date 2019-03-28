using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using ShortRent.Core.Domain;
using ShortRent.Resource;
using ShortRent.Resource.Mvc;

namespace ShortRent.Web.Validator.Persons
{
    public class PersonValidator:AbstractValidator<Person>
    {
       public PersonValidator()
        {
            RuleFor(p => p.Name).NotNull().Length(5,10).WithLocalizedMessage(()=>Resources.Scope);
            RuleFor(p => p.Birthday).NotNull();
        }
    }
}