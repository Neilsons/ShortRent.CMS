using FluentValidation;
using ShortRent.Web.Areas.ShortWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortRent.Web.Validator.WebRegisters
{
    public class uploadImageValidator : AbstractValidator<UploadWebImage>
    {
        public uploadImageValidator()
        {

        }
    }
}