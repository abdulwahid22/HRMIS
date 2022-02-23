using FluentValidation;
using NHCM.Application.Infrastructure.Validations;
using NHCM.Application.Recruitment.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Recruitment.Validators
{
    public class SavePersonRelativesCommandValidator : AbstractValidator<SavePersonRelatives>
    {
        public SavePersonRelativesCommandValidator()
        {
            // FirstName
            //RuleFor(p => p.LastName).NotNull().NotEmpty().MinimumLength(3).WithMessage("تخلص کارمند باید حد اقل دارای سه حرف باشد");
            //RuleFor(p => p.LastName).MaximumLength(50).WithMessage("تخلص کارمند میتواند حد اکثر دارای پنجاه حرف باشد");

            RuleFor(r => r.FirstName).NotNull().NotEmpty().MinimumLength(3).WithMessage("نام باید حد اکثر دارای سه حرف باشد");
            RuleFor(r => r.FirstName).MaximumLength(50) .WithMessage("نام باید حد اقل دارای سه حرف باشد");
            RuleFor(r => r.FirstName)
              .CannotInclude(ValidationHelper.ForbiddenSymbols)
              .WithMessage(" نام نمی تواند یکی از حروف ذیل را داشته باشد " + ValidationHelper.StringyfyList(ValidationHelper.ForbiddenSymbols));
            RuleFor(r => r.FirstName)
                .NoDigit()
                .WithMessage("نام نمیتواند دارای ارقام باشد");

            // LastName

            RuleFor(r => r.LastName).NotNull().NotEmpty().MinimumLength(3).WithMessage("تخلص باید حد اقل دارای سه حرف باشد");
            RuleFor(r => r.LastName).MinimumLength(3).WithMessage("تخلص میتواند حد اقل دارای پنجاه حرف باشد");
            RuleFor(r => r.LastName).MaximumLength(50) .WithMessage("تخلص میتواند حد اکثر دارای پنجاه حرف باشد");
            RuleFor(r => r.LastName).CannotInclude(ValidationHelper.ForbiddenSymbols)
                .WithMessage(" تخلص نمی تواند یکی از حروف ذیل را داشته باشد " + ValidationHelper.StringyfyList(ValidationHelper.ForbiddenSymbols));



            // FatherName

            RuleFor(r => r.FatherName).NotNull() .NotEmpty() .MinimumLength(3).WithMessage("ولد حدااقل دارای سه حرف بوده میتواند");
            RuleFor(r => r.FatherName).MaximumLength(50).WithMessage("ولد حداکثر دارای پنجاه حرف بوده میتواند");
            RuleFor(r => r.FatherName).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("ولد نمی تواند یکی از حروف ذیل را داشته باش");
            RuleFor(r => r.FatherName).NoDigit().WithMessage(" ولد  نمی تواند دارای ارقام باشد");

            //GrandFaterhName
            RuleFor(r => r.GrandFatherName)
                .NotNull().NotEmpty().MinimumLength(3).WithMessage(" نام پدر کلان حد اقل دارای سه حرف بوده میتواند");
            RuleFor(r => r.GrandFatherName).MaximumLength(50).WithMessage(" نام پدر کلان حد اکثر دارای پنجاه حرف بوده میتواند");

            RuleFor(r => r.GrandFatherName).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("نام پدر کلان نمی تواند یکی از حروف ذیل را داشته باش");
            RuleFor(r => r.FatherName).NoDigit().WithMessage(" نام پدر کلان نمی تواند دارای ارقام باشد");

            //Telephon NO
            RuleFor(r => r.ContactInfo)
                .NotNull() 
                .NotEmpty()
                .MinimumLength(10).WithMessage(" نمبر تلیفون دارای ده عدد بوده میتواند");
            RuleFor(r => r.ContactInfo).MaximumLength(10).WithMessage(" نمبر تلیفون دارای ده عدد بوده میتواند");
            RuleFor(r => r.ContactInfo).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("نمبر تلیفون نمی تواند یکی از حروف ذیل را داشته باش");
            RuleFor(r => r.ContactInfo).NotEmpty().WithMessage("نمبر تلیفون تنهاعدد بوده میتواند");

            //Email address
            RuleFor(r => r.EmailAddress)
                .NotNull()
                .NotEmpty();
            RuleFor(r => r.EmailAddress).EmailAddress().WithMessage("ایمیل آدرس درست نمیباشد");
            RuleFor(r => r.EmailAddress).NotEmpty().WithMessage("ایمیل آدرس درست نیست");


            //profession
            RuleFor(r => r.Profession)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5).WithMessage(" شغل/پیشه اقل دارای پنج حرف بوده میتواند");
            RuleFor(r => r.Profession).MaximumLength(150).WithMessage(" شغل/پیشه اکثر دارای یک صد و پنجاه حرف بوده میتواند");
            RuleFor(r => r.Profession).NoDigit().WithMessage(" شغل/پیشه نمی تواند دارای ارقام باشد");
            RuleFor(r => r.Profession).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("شغل/پیشه نمی تواند یکی از حروف ذیل را داشته باش");



        }
    }
}
