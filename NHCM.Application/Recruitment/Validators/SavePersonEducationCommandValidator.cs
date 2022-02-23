using FluentValidation;
using NHCM.Application.Infrastructure.Validations;
using NHCM.Application.Recruitment.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Recruitment.Validators
{
    public class SavePersonEducationCommandValidator : AbstractValidator<SavePersonEducation>
    {
        public SavePersonEducationCommandValidator()
        {
            //Istitute

            RuleFor(e => e.Institute).NotNull().NotEmpty().MinimumLength(3).WithMessage("موسسه تحصیلی حد اقل دارای سه حرف باشد");
            RuleFor(e => e.Institute).MaximumLength(50).WithMessage("موسسه تحصیلی میتواند حد اکثر دارای پنجاه حرف باشد");
            RuleFor(e => e.Institute)
                .CannotInclude(ValidationHelper.ForbiddenSymbols)
                .WithMessage(" موسسه تحصیلی نمی تواند یکی از حروف ذیل را داشته باشد " + ValidationHelper.StringyfyList(ValidationHelper.ForbiddenSymbols));
            RuleFor(e => e.Institute)
                .NoDigit()
                .WithMessage("موسسه تحصیلید نمیتواند دارای ارقام باشد"); 


            //document NO
            RuleFor(e => e.OfficialDocumentNo)
                .NotNull()
                .NotEmpty()
                .MinimumLength(1).WithMessage(" نمبر سند رسمی حداقل دارای یک عدد بوده میتواند");
            RuleFor(e => e.OfficialDocumentNo).MaximumLength(10).WithMessage(" نمبر سند رسمی حد اکثر دارای ده عدد بوده میتواند"); 
            //RuleFor(e => e.OfficialDocumentNo).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage(""); 


            //Faculty
            RuleFor(e => e.Faculty)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5).WithMessage(" پوهنځی حداقل دارای پنج حرف بوده میتواند");
            RuleFor(e => e.Faculty).MaximumLength(150) .WithMessage(" پوهنځی حد اکثر دارای یک صد و پنجاه حرف بوده میتواند"); 
            RuleFor(e => e.Faculty).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("پوهنځی نمیتواند یکی از حرف های ذیل را داشته باشد");
            RuleFor(e => e.Faculty).NoDigit().WithMessage(" پوهنځی نمی تواند دارای ارقام باشد");


            //deportment
            RuleFor(e => e.Department)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5).WithMessage(" دیپارتمنت حداقل دارای پنج حرف بوده میتواند");
            RuleFor(e => e.Department).MaximumLength(150).WithMessage(" دیپارتمنت حد اکثر دارای صد حرف بوده میتواند");
            RuleFor(e => e.Department).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("دیپارتمنت نمیتواند یکی از حرف های ذیل را داشته باشد");
            RuleFor(e => e.Department).NoDigit().WithMessage(" دیپارتمنت نمی تواند دارای ارقام باشد");

            //Major
            RuleFor(e => e.Major)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5).WithMessage(" تخصص حداقل دارای پنج حرف بوده میتواند");
            RuleFor(e => e.Major).MaximumLength(100).WithMessage(" تخصص حد اکثر دارای  صد حرف بوده میتواند");
            RuleFor(e => e.Major).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("تخصص نمیتواند یکی از حرف های ذیل را داشته باشد");
            RuleFor(e => e.Major).NoDigit().WithMessage(" تخصص نمی تواند دارای ارقام باشد");

            //cource
            RuleFor(e => e.Course)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5).WithMessage(" رشته حداقل دارای پنج حرف بوده میتواند");
            RuleFor(e => e.Course).MaximumLength(100) .WithMessage(" رشته حد اکثر دارای  صد حرف بوده میتواند");
            RuleFor(e => e.Course).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("رشته نمیتواند یکی از حرف های ذیل را داشته باشد");
            RuleFor(e => e.Course).NoDigit().WithMessage(" رشته نمی تواند دارای ارقام باشد");

            //rmarks
            RuleFor(e => e.Remarks)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5).WithMessage(" ملاحضات حداقل دارای پنج حرف بوده میتواند");
            RuleFor(e => e.Remarks).MaximumLength(150).WithMessage(" ملاحضات حد اکثر دارای یک صد و پنجاه حرف بوده میتواند"); 
            RuleFor(e => e.Remarks).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("ملاحضات نمیتواند یکی از حرف های ذیل را داشته باشد");
            RuleFor(e => e.Remarks).NoDigit().WithMessage(" ملاحضات نمی تواند دارای ارقام باشد");


            //start date
            RuleFor(e => e.StartDate)
                .NotNull()
                .NotEmpty() 
                .WithMessage("تاریخ آغاز تحصیل خالی بوده نمیتواند");

            //end date
            RuleFor(e => e.EndDate)
                .NotNull()
                .NotEmpty()
                .WithMessage("تاریخ ختم تحصیل خالی بوده نمیتواند");


        }
    } 
}
