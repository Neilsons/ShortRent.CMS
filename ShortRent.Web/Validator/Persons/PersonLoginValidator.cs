using FluentValidation;
using ShortRent.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Validator
{
    public class PersonLoginValidator: AbstractValidator<PersonLoginModel>
    {
        public PersonLoginValidator()
        {
            this.RuleFor(p => p.Name).Length(1,20).NotNull();
            this.RuleFor(p => p.PassWord).Length(1, 120).NotNull();
        }
        
    }
}