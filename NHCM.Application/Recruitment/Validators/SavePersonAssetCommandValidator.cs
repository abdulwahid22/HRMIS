using FluentValidation;
using NHCM.Application.Infrastructure.Validations;
using NHCM.Application.Recruitment.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Recruitment.Validators
{
    public class SavePersonAssetCommandValidator: AbstractValidator<SavePersonAssetCommand>
    {
        public SavePersonAssetCommandValidator()
        {
            //coust
            RuleFor(a => a.Value)
                .NotNull()
                .NotEmpty().WithMessage("ارزش خالی بوده نمیتواند");    
            //RuleFor(a => a.Value).DigitOnly().WithMessage("ارزش تنها عدد بوده میتواند");
             

            //description
            RuleFor(a => a.Description)
                .NotNull()
                .NotEmpty()
                .MinimumLength(10).WithMessage(" تفصیل حداقل دارای ده حرف بوده میتواند");
            RuleFor(a => a.Description).MaximumLength(100) .WithMessage(" تفصیل  حد اکثر دارای صد حرف بوده میتواند");
            RuleFor(a => a.Description).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("تفصیل نمی تواند یکی از حروف ذیل را داشته باشد");
            RuleFor(a => a.Description).DariTextOnly().WithMessage("تفصیل تنها به دری بوده میتواند");



        }
    }
}
