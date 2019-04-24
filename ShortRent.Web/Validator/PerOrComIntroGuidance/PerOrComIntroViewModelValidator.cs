using FluentValidation;
using ShortRent.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Validator
{
    public class PerOrComIntroViewModelValidator:AbstractValidator<PerOrComIntroGuidanceViewModel>
    {
        public PerOrComIntroViewModelValidator()
        {
            this.RuleFor(p => p.QuestionMsg).Length(1, 200).NotNull();
            this.RuleFor(p => p.Type).NotNull();
        }

    }
}