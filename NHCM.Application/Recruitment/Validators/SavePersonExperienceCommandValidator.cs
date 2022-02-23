using FluentValidation;
using NHCM.Application.Infrastructure.Validations;
using NHCM.Application.Recruitment.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Recruitment.Validators
{
    public class SavePersonExperienceCommandValidator : AbstractValidator<SavePersonExperienceCommand>
    {
        public SavePersonExperienceCommandValidator()
        {
            //organizaiton
            RuleFor(ex => ex.Organization)
                .NotNull()
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(150)
                .WithMessage(" موسسه حداقل دارای ده و حد اکثر دارای یک صد و پنجاه حرف بوده میتواند");
            RuleFor(ex => ex.Organization).NoDigit().WithMessage(" موسسه نمی تواند دارای ارقام باشد");
            RuleFor(ex => ex.Organization).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("موسسه نمی تواند یکی از حروف ذیل را داشته باش");

            //designation
            RuleFor(ex => ex.Designation)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(150)
                .WithMessage(" وظیفه حداقل دارای پنج و حد اکثر دارای یک صد و پنجاه حرف بوده میتواند");
            RuleFor(ex => ex.Designation).NoDigit().WithMessage(" وظیفه نمی تواند دارای ارقام باشد");
            RuleFor(ex => ex.Designation).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("موسسه نمی تواند یکی از حروف ذیل را داشته باش");


            // contact
            RuleFor(ex => ex.ContactInfo)
                .NotNull()
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(10)
                .WithMessage(" شماره تماس حداقل دارای ده حرف بوده میتواند"); 
            RuleFor(ex => ex.ContactInfo).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("موسسه نمی تواند یکی از حروف ذیل را داشته باش");

            //start date
            RuleFor(ex => ex.StartDate)
                .NotNull()
                .NotEmpty()
                .WithMessage("تاریخ آغاز خالی بوده نمیتواند");

            //end date
            RuleFor(ex => ex.EndDate)
                .NotNull()
                .NotEmpty()
                .WithMessage("تاریخ ختم خالی بوده نمیتواند");

            //rmarks
            RuleFor(ex => ex.Remarks)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(150)
                .WithMessage(" ملاحضات حداقل دارای پنج و حد اکثر دارای یک صد و پنجاه حرف بوده میتواند");
            RuleFor(ex => ex.Remarks).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("موسسه نمی تواند یکی از حروف ذیل را داشته باش");
            RuleFor(ex => ex.Remarks).NoDigit().WithMessage(" ملاحضات نمی تواند دارای ارقام باشد");
        }
    }
}
