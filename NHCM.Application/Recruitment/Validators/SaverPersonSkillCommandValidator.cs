using FluentValidation;
using NHCM.Application.Infrastructure.Validations;
using NHCM.Application.Recruitment.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Recruitment.Validators
{
    public class SaverPersonSkillCommandValidator : AbstractValidator<SavePersonSkillsCommand>
    {
        public SaverPersonSkillCommandValidator()
        {
            //start date
            RuleFor(s => s.StartDate)
                .NotNull()
                .NotEmpty()
                .WithMessage("تاریخ آغاز تحصیل خالی بوده نمیتواند");
            //end date
            RuleFor(s => s.EndDate)
                .NotNull()
                .NotEmpty()
                .WithMessage("تاریخ ختم تحصیل خالی بوده نمیتواند");

            //CertifiedFrom
            RuleFor(s => s.CertifiedFrom)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100)
                .WithMessage(" مرجع حداقل دارای سخ و حد اکثر دارای  صد حرف بوده میتواند");
            RuleFor(s => s.CertifiedFrom).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("مرجع نمی تواند یکی از حروف ذیل را داشته باش");
            RuleFor(s => s.CertifiedFrom).NoDigit().WithMessage(" مرجع نمی تواند دارای ارقام باشد");

            //rmarks
            RuleFor(s => s.Remarks)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(150)
                .WithMessage(" ملاحضات حداقل دارای پنج و حد اکثر دارای یک صد و پنجاه حرف بوده میتواند");
            RuleFor(s => s.Remarks).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("ملاحضات نمی تواند یکی از حروف ذیل را داشته باش");
            RuleFor(s => s.Remarks).NoDigit().WithMessage(" ملاحضات نمی تواند دارای ارقام باشد");


        }
    }
}
