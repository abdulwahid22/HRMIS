using FluentValidation;
using NHCM.Application.Infrastructure.Validations;
using NHCM.Application.Recruitment.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Recruitment.Validators
{
    public class SavePersonPublicationCommandValidator : AbstractValidator<SavePersonPublicationCommand>
    {
        public SavePersonPublicationCommandValidator()
        {
            //subject
            RuleFor(pu => pu.Subject)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(50)
                .WithMessage(" مضمون حداقل دارای سه و حد اکثر دارای پنجاه حرف بوده میتواند");
            RuleFor(pu => pu.Subject).NoDigit().WithMessage(" مضمون نمی تواند دارای ارقام باشد");
            RuleFor(pu => pu.Subject).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("مضمون نمی تواند یکی از حروف ذیل را داشته باش");
             
            // No of pages
            RuleFor(pu => pu.NoofPages)
                .NotNull()
                .NotEmpty() 
                .WithMessage("تعداد صفحات خالی بوده نمیتواند");

            // ISBN
            RuleFor(pu => pu.Isbn)
                .NotNull()
                .NotEmpty()
                .WithMessage("خالی بوده نمیتواند ISBN ");

            //publish date
            RuleFor(pu => pu.PublishDate)
                .NotNull()
                .NotEmpty()
                .WithMessage("تاریخ چاپ خالی بوده نمیتواند");

        }
    }
}
