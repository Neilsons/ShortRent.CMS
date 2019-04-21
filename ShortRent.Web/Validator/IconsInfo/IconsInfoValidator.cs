using FluentValidation;
using ShortRent.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web
{
    public class IconsInfoValidator : AbstractValidator<IconViewModel>
    {
        public IconsInfoValidator()
        {
            RuleFor(p => p.Prefix).NotNull().Length(1, 50);
            RuleFor(p => p.Content).NotNull().Length(1, 50);
        }
    }
}