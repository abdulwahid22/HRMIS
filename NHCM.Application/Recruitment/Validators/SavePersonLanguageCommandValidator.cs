using FluentValidation;
using NHCM.Application.Recruitment.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Recruitment.Validators
{
    public class SavePersonLanguageCommandValidator : AbstractValidator<SavePersonLanguageCommand>
    {
        public SavePersonLanguageCommandValidator()
        {

        }
    }
}
