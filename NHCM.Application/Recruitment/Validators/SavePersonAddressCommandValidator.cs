using FluentValidation;
using NHCM.Application.Infrastructure.Validations;
using NHCM.Application.Recruitment.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Recruitment.Validators
{
    public class SavePersonAddressCommandValidator : AbstractValidator<SavePersonAddressCommand>
    {
        public SavePersonAddressCommandValidator()
        {
            //Village
            RuleFor(a => a.Village)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3).WithMessage(" قریه اصلی حداقل دارای سه حرف بوده میتواند");
            RuleFor(a => a.Village).MaximumLength(50) .WithMessage(" قریه اصلی حداکثر دارای پنجاه حرف بوده میتواند");
            RuleFor(a => a.Village).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("قریه اصلی نمیتواند یکی از حرف های ذیل را داشته باشد");
            RuleFor(a => a.Village).DariTextOnly().WithMessage("قریه تنها به دری بوده میتواند");
           
            //Current Village
            RuleFor(a => a.Cvillage)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3).WithMessage(" قریه فعلی حداقل دارای سه حرف بوده میتواند");
            RuleFor(a => a.Cvillage).MaximumLength(50).WithMessage(" قریه فعلی حد اکثر دارای پنجاه حرف بوده میتواند"); 
            RuleFor(a => a.Cvillage).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("قریه فعلی نمیتواند یکی از حرف های ذیل را داشته باشد");
            RuleFor(a => a.Cvillage).DariTextOnly().WithMessage("قریه فعلی تنها به دری بوده میتواند");

            //Street No
            RuleFor(a => a.StreetNo)
                .NotNull()
                .NotEmpty()
                .MinimumLength(1).WithMessage(" نمبر سرک حداقل دارای یک عدد بوده میتواند");
            RuleFor(a => a.StreetNo).MaximumLength(5).WithMessage(" نمبر سرک حد اکثر دارای پنج عدد بوده میتواند");
            RuleFor(a => a.StreetNo).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("نمبر سرک نمیتواند یکی از حرف های ذیل را داشته باشد");
            RuleFor(a => a.StreetNo).NotEmpty().WithMessage("نمبر سرک خالی بوده نمیتواند");
            RuleFor(a => a.StreetNo).DigitOnly().WithMessage("نمبر سرک تنها عدد بوده میتواند");

            //House No
            RuleFor(a => a.HouseNo)
                .NotNull()
                .NotEmpty()
                .MinimumLength(1).WithMessage(" نمبر خانه حداقل دارای یک عدد بوده میتواند");
            RuleFor(a => a.HouseNo).MaximumLength(5).WithMessage(" نمبر خانه حد اکثر دارای پنج عدد بوده میتواند");
            RuleFor(a => a.HouseNo).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("نمبر خانه نمیتواند یکی از حرف های ذیل را داشته باشد");
            RuleFor(a => a.HouseNo).NotEmpty().WithMessage("نمبر خانه خالی بوده نمیتواند");
            RuleFor(a => a.HouseNo).DigitOnly().WithMessage("نمبر خانه تنهاعدد بوده میتواند");

            //Mobile No
            RuleFor(a => a.Mobile)
                .NotNull()
                .NotEmpty()
                .MinimumLength(10).WithMessage(" نمبر موبایل حداقل دارای ده عدد بوده میتواند");
            RuleFor(a => a.Mobile).MaximumLength(10) .WithMessage(" نمبر موبایل حد اگثر دارای ده عدد بوده میتواند");
            RuleFor(a => a.Mobile).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage("نمبر موبایل نمیتواند یکی از حرف های ذیل را داشته باشد");
            RuleFor(a => a.Mobile).MobileNumberOnly().WithMessage("نمبر موبایل درست نمیباشد لطفا شماره را درست وارد نمایید");
            RuleFor(a => a.Mobile).NotEmpty().WithMessage("نمبر موبایل خالی بوده نمیتواند");

            //Telephon No
            RuleFor(a => a.Phone)
                .NotNull()
                .NotEmpty()
                .MinimumLength(10).WithMessage(" نمبر تلیفون دارای ده عدد بوده میتواند");
            RuleFor(a => a.Phone).MaximumLength(10).WithMessage(" نمبر تلیفون دارای ده عدد بوده میتواند");
            RuleFor(a => a.Phone).CannotInclude(ValidationHelper.ForbiddenSymbols).WithMessage(" دارای ده عدد بوده میتواند نمیتواند یکی از حرف های ذیل را داشته باشد");
            RuleFor(a => a.Phone).MobileNumberOnly().WithMessage("نمبر تلیفون درست نمیباشد لطفا شماره را درست وارد نمایید");
            RuleFor(a => a.Phone).NotEmpty().WithMessage("نمبر تلیفون خالی بوده نمیتواند");

            //Email address
            RuleFor(a => a.EmailAddress)
                .NotNull()
                .NotEmpty();
            RuleFor(a => a.EmailAddress).EmailAddress().WithMessage("ایمیل آدرس درست نمیباشد");
            RuleFor(a => a.EmailAddress).NotEmpty().WithMessage("ایمیل آدرس درست نیست");



        }
    }
}
