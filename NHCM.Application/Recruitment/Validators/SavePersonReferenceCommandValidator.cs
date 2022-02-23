using FluentValidation;
using NHCM.Application.Infrastructure.Validations;
using NHCM.Application.Recruitment.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Recruitment.Validators
{
    public class SavePersonReferenceCommandValidator : AbstractValidator<SavePersonReferenceCommand>
    {
        public SavePersonReferenceCommandValidator()
        {
            // FirstName

            RuleFor(r => r.FirstName)
                .NotNull().NotEmpty()
                .MinimumLength(3) 
                .MaximumLength(50)
                .WithMessage("نام تضمین کننده باید حد اقل دارای سه و حد اکثر دارای پنجاه حرف باشد");
              RuleFor(r => r.FirstName)
                .CannotInclude(ValidationHelper.ForbiddenSymbols)
                .WithMessage(" نام کارمند نمی تواند یکی از حروف ذیل را داشته باشد " + ValidationHelper.StringyfyList(ValidationHelper.ForbiddenSymbols));
            RuleFor(r => r.FirstName)
                .NoDigit()
                .WithMessage("نام تضمین کننده نمیتواند دارای ارقام باشد"); 

            // LastName

            RuleFor(r => r.LastName).NotNull().NotEmpty().MinimumLength(3).WithMessage("تخلص کارمند باید حد اقل دارای سه حرف باشد");
            RuleFor(r => r.LastName)
                .MinimumLength(3)
                .MaximumLength(50)
                .WithMessage("تخلص تضمین کننده میتواند حد اکثر دارای پنجاه حرف باشد");
            RuleFor(r => r.LastName)
                .CannotInclude(ValidationHelper.ForbiddenSymbols)
                .WithMessage(" تخلص تضمین کننده نمی تواند یکی از حروف ذیل را داشته باشد " + ValidationHelper.StringyfyList(ValidationHelper.ForbiddenSymbols));
           


            // FatherName

            RuleFor(r => r.FatherName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(50)
                .WithMessage("ولد تضمین کننده حداقل دارای سه و حد اکثر دارای پنجاه حرف بوده میتواند"); 
            RuleFor(r => r.FatherName).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("ولد تضمین کننده نمی تواند یکی از حروف ذیل را داشته باش");
            RuleFor(r => r.FatherName).NoDigit().WithMessage(" ولد تضمین کننده نمی تواند دارای ارقام باشد");

            //GrandFaterhName
            RuleFor(r => r.GrandFatherName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(50)
                .WithMessage(" نام پدر کلان حداقل دارای سه و حد اکثر دارای پنجاه حرف بوده میتواند");

            RuleFor(r => r.GrandFatherName).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("نام پدر کلان نمی تواند یکی از حروف ذیل را داشته باش"); 
            RuleFor(r => r.FatherName).NoDigit().WithMessage(" نام پدر کلان تضمین کنند نمی تواند دارای ارقام باشد");


            //Telephon NO
            RuleFor(r => r.TelephoneNo)
                .NotNull()
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(10)
                 .WithMessage(" نمبر تلیفون دارای ده عدد بوده میتواند");
            RuleFor(r => r.TelephoneNo).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("نمبر تلیفون نمی تواند یکی از حروف ذیل را داشته باش");
            RuleFor(r => r.TelephoneNo).NotEmpty().WithMessage("نمبر تلیفون تنهاعدد بوده میتواند");

            //designation
            RuleFor(r => r.Occupation)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(150)
                .WithMessage(" وظیفه حداقل دارای پنج و حد اکثر دارای یک صد و پنجاه حرف بوده میتواند");
            RuleFor(r => r.Occupation).NoDigit().WithMessage(" وظیفه نمی تواند دارای ارقام باشد");
            RuleFor(r => r.Occupation).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("وظیفه نمی تواند یکی از حروف ذیل را داشته باش");

            //Organization
            RuleFor(r => r.Organization)
                .NotNull()
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(150)
                .WithMessage(" نام ارگان حداقل دارای ده و حد اکثر دارای یک صد و پنجاه حرف بوده میتواند");
            RuleFor(r => r.Organization).NoDigit().WithMessage(" نام ارگان نمی تواند دارای ارقام باشد");
            RuleFor(r => r.Organization).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("نام ارگان نمی تواند یکی از حروف ذیل را داشته باش");


            //District
            RuleFor(r => r.District)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(50)
                .WithMessage(" ولسوالی حداقل دارای سه و حد اکثر دارای پنجاه حرف بوده میتواند");

            RuleFor(r => r.District).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("ولسوالی نمی تواند یکی از حروف ذیل را داشته باش");
            RuleFor(r => r.District).DariTextOnly().WithMessage("ولسوالی تنها به دری بوده میتواند");

            // relationship

            RuleFor(r => r.RelationShip).NotNull().NotEmpty().MinimumLength(3).WithMessage("رابطه باید حد اقل دارای سه حرف باشد");
            RuleFor(r => r.RelationShip)
                .MaximumLength(50)
                .WithMessage("رابطه میتواند حد اکثر دارای پنجاه حرف باشد");
            RuleFor(r => r.RelationShip)
                .CannotInclude(ValidationHelper.ForbiddenSymbols)
                .WithMessage(" رابطه نمی تواند یکی از حروف ذیل را داشته باشد " + ValidationHelper.StringyfyList(ValidationHelper.ForbiddenSymbols));
        

        }
    }
}
