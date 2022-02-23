using FluentValidation;
using NHCM.Application.Infrastructure.Validations;
using NHCM.Application.Lookup.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Lookup.Validators
{
    public class SaveOrganizationCommandValidator : AbstractValidator<SaveOrganiztionCommand>
    {
        public SaveOrganizationCommandValidator()
        {
            // Dari

            RuleFor(o => o.Dari).NotNull().NotEmpty().MinimumLength(3).WithMessage("نام ارگان باید حد اقل دارای سه حرف باشد");
            RuleFor(o => o.Dari).MaximumLength(50).WithMessage("نام ارگان میتواند حد اکثر دارای پنجاه حرف باشد");
            RuleFor(o => o.Dari)
                .CannotInclude(ValidationHelper.ForbiddenSymbols)
                .WithMessage(" نام ارگان نمی تواند یکی از حروف ذیل را داشته باشد " + ValidationHelper.StringyfyList(ValidationHelper.ForbiddenSymbols));
            RuleFor(o => o.Dari)
                .NoDigit()
                .WithMessage("نام ارگان نمیتواند دارای ارقام باشد");
            RuleFor(o => o.Dari).DariTextOnly().WithMessage("نام دری ارگان تنها به دری بوده میتواند");
        }
    }
}
