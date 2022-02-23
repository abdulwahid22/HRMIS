using FluentValidation;
using NHCM.Application.Infrastructure.Validations;
using NHCM.Application.Recruitment.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Recruitment.Validators
{
    public class SavePersonTravelCommandValidator : AbstractValidator<SavePersonTravelCommand>
    {
        public SavePersonTravelCommandValidator()
        {
            //place
            RuleFor(t => t.Place)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(50)
                .WithMessage(" محل حداقل دارای سه و حد اکثر دارای پنجاه حرف بوده میتواند"); 
            RuleFor(t => t.Place).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("محل نمی تواند یکی از حروف ذیل را داشته باش");


            //start date
            RuleFor(t => t.TravelDate)
                .NotNull()
                .NotEmpty()
                .WithMessage("تاریخ سفر خالی بوده نمیتواند");

            //end date
            RuleFor(t => t.ReturnDate)
                .NotNull()
                .NotEmpty()
                .WithMessage("تاریخ برگشت خالی بوده نمیتواند");

            //rmarks
            RuleFor(t => t.Reason)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(150)
                .WithMessage(" ملاحضات حداقل دارای پنج و حد اکثر دارای یک صد و پنجاه حرف بوده میتواند");
            RuleFor(t => t.Reason).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("ملاحضات نمی تواند یکی از حروف ذیل را داشته باش");
            RuleFor(t => t.Reason).NoDigit().WithMessage(" ملاحضات نمی تواند دارای ارقام باشد");

        }
    }
}
