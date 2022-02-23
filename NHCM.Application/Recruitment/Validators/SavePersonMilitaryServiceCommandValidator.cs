using FluentValidation;
using NHCM.Application.Infrastructure.Validations;
using NHCM.Application.Recruitment.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Recruitment.Validators
{
    public class SavePersonMilitaryServiceCommandValidator : AbstractValidator<SavePersonMilitaryServiceCommand>
    {
        public SavePersonMilitaryServiceCommandValidator()
        {
            //start date
            RuleFor(ms => ms.StartDate)
                .NotNull()
                .NotEmpty()
                .WithMessage("تاریخ آغاز خالی بوده نمیتواند");
            //end date
            RuleFor(ms => ms.EndDate)
                .NotNull()
                .NotEmpty()
                .WithMessage("تاریخ ختم خالی بوده نمیتواند");

            //rmarks
            RuleFor(ms => ms.Remark)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(150)
                .WithMessage(" ملاحضات حداقل دارای پنج و حد اکثر دارای یک صد و پنجاه حرف بوده میتواند");
            RuleFor(ms => ms.Remark).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("ملاحضات نمی تواند یکی از حروف ذیل را داشته باش");
            RuleFor(ms => ms.Remark).NoDigit().WithMessage(" ملاحضات نمی تواند دارای ارقام باشد");

        }
    }
}
