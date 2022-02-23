using FluentValidation;
using NHCM.Application.Infrastructure.Validations;
using NHCM.Application.Recruitment.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Recruitment.Validators
{
    public class SavePersonHealthCommandValidator : AbstractValidator<SavePersonHealthReportCommand>
    {
        public SavePersonHealthCommandValidator()
        {
            //report date
            RuleFor(ex => ex.ReportDate)
                .NotNull()
                .NotEmpty()
                .WithMessage("تاریخ راپور خالی بوده نمیتواند");

            //rmarks
            RuleFor(ex => ex.Remarks)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(150)
                .WithMessage(" ملاحضات حداقل دارای پنج و حد اکثر دارای یک صد و پنجاه حرف بوده میتواند");
            RuleFor(ex => ex.Remarks).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("ملاحضات نمی تواند یکی از حروف ذیل را داشته باش");
            RuleFor(ex => ex.Remarks).NoDigit().WithMessage(" ملاحضات نمی تواند دارای ارقام باشد");
        }
    }
}
