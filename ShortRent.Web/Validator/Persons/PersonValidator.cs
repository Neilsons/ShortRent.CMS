using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using ShortRent.Core.Domain;

namespace ShortRent.Web.Validator.Persons
{
    public class PersonValidator:AbstractValidator<Person>
    {
       public PersonValidator()
        {
            RuleFor(p => p.Name).NotNull().WithMessage("姓名不能为空").Length(5,10);
            RuleFor(p => p.Birthday).NotNull();
        }
    }
}