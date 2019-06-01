using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Validator.PublishMsg
{
    public class StringPublishMsgValidator : AbstractValidator<String>
    {
        public StringPublishMsgValidator()
        {
            this.RuleFor(p => p).NotNull();
        }

    }
}