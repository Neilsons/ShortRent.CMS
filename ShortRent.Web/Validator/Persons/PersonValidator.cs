using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using ShortRent.Core.Domain;
using ShortRent.Web.Properties;

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