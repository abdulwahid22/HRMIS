using System;
using System.Collections.Generic;
using System.Text;
using NHCM.Application.Infrastructure.Validations;
using FluentValidation;
using NHCM.Application.Recruitment.Commands;


namespace NHCM.Application.Recruitment.Validators
{
    public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator()
        {

            // FirstName
            
            RuleFor(p => p.FirstName).NotNull().NotEmpty().MinimumLength(3).WithMessage("نام کارمند باید حد اقل دارای سه حرف باشد" );
            RuleFor(p => p.FirstName).MaximumLength(50).WithMessage("نام کارمند میتواند حد اکثر دارای پنجاه حرف باشد" );
            RuleFor(p => p.FirstName)
                .CannotInclude(ValidationHelper.ForbiddenSymbols)
                .WithMessage(" نام کارمند نمی تواند یکی از حروف ذیل را داشته باشد " + ValidationHelper.StringyfyList(ValidationHelper.ForbiddenSymbols) );
            RuleFor(p => p.FirstName)
                .NoDigit()
                .WithMessage("نام کارمند نمیتواند دارای ارقام باشد" );
            RuleFor(p => p.FirstName).DariTextOnly().WithMessage("نام دری کارمند تنها به دری بوده میتواند" );
              
            // LastName

            RuleFor(p => p.LastName).NotNull().NotEmpty().MinimumLength(3).WithMessage("تخلص کارمند باید حد اقل دارای سه حرف باشد" );
            RuleFor(p => p.LastName).MaximumLength(50).WithMessage("تخلص کارمند میتواند حد اکثر دارای پنجاه حرف باشد" );
            RuleFor(p => p.LastName)
                .CannotInclude(ValidationHelper.ForbiddenSymbols)
                .WithMessage(" تخلص کارمند نمی تواند یکی از حروف ذیل را داشته باشد " + ValidationHelper.StringyfyList(ValidationHelper.ForbiddenSymbols) );
            RuleFor(p => p.LastName).NoDigit()
                .WithMessage("تخلص کارمند نمیتواند دارای ارقام باشد" );
            RuleFor(p => p.LastName).DariTextOnly()
                .WithMessage("تخلص دری کارمند تنها به دری بوده میتواند" );


            // FatherName

            RuleFor(p => p.FatherName)
                .NotNull()
                .NotEmpty()

                .MinimumLength(3). WithMessage("ولد کارمند حداقل دارای سه حرف بوده میتواند")
                .MaximumLength(50).WithMessage("ولد کارمند حدااکثر دارای پنجاه حرف بوده میتواند");

            RuleFor(p => p.FatherName).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("ولد کارمند نمی تواند یکی از حروف ذیل را داشته باشد");

            RuleFor(p => p.FatherName).DariTextOnly().WithMessage("ولد دری کارمند تنها به دری بوده میتواند");
            RuleFor(p => p.FatherName).NoDigit().WithMessage(" ولد دری کارمند نمی تواند دارای ارقام باشد");

            //GrandFaterhName
            RuleFor(p => p.GrandFatherName)
                .NotNull()
                .NotEmpty()
                 .MinimumLength(3).WithMessage("نام پدر کلان حداقل دارای سه حرف بوده میتواند")
                .MaximumLength(50).WithMessage(" نام پدر کلان اکثر دارای پنجاه حرف بوده میتواند"); 

            RuleFor(p => p.GrandFatherName).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("نام پدر کلان نمی تواند یکی از حروف ذیل را داشته باشد");
            RuleFor(p => p.GrandFatherName).DariTextOnly().WithMessage("نام پدر کلان دری کارمند تنها به دری بوده میتواند");
            RuleFor(p => p.FatherName).NoDigit().WithMessage(" نام پدر کلان دری کارمند نمی تواند دارای ارقام باشد");


            //DOB
            RuleFor(p => p.DateOfBirth)
                .NotNull()
                .NotEmpty()
                .WithMessage("تاریخ تولد خالی بوده نمیتواند");
  
        }

    }

  
}
