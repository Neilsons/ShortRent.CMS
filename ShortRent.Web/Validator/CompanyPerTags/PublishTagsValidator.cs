using FluentValidation;
using ShortRent.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Validator
{
    public class PublishTagsValidator : AbstractValidator<PublishTagsViewModel>
    {
        public PublishTagsValidator()
        {
            RuleFor(p => p.Name).NotNull().Length(1, 20);
            RuleFor(p => p.Color).NotNull().Length(1, 10);
            RuleFor(p => p.TagOrder).NotNull();
        }
    }
}