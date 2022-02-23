using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Infrastructure.Identity
{
   public class IdentityLocalizedErrorDescribers : IdentityErrorDescriber
    {


        public override IdentityError DefaultError()
        {
            return new IdentityError()
            {
                Code = nameof(DefaultError),
                Description = " درخواست شما با مشکل تخنیکی مواجه میباشد لطفا با مدیر سیستم به تماس شوید"
            };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError() {
                Code = nameof(DuplicateEmail),
                Description = $"{email} قبلا استفاده شده لطفا ایمیل متفاوت را استفاده کنید  "
            };
        }

        public override IdentityError DuplicateRoleName(string role)
        {
            return new IdentityError()
            {
                Code = nameof(DuplicateRoleName),
                Description = $"{role} قبلا استفاده شده لطفا نام متفاوت را استفاده کنید  "
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError()
            {
                Code = nameof(DuplicateUserName),
                Description = $"{userName} قبلا استفاده شده لطفا نام کاربری متفاوت را استفاده کنید  "
            };
        }

        public override IdentityError InvalidEmail(string email)
        {
            return new IdentityError()
            {
                Code = nameof(InvalidEmail),
                Description = $"{email} نادرست میباشد  لطفا  ایمیل درست را استفاده کنید  "
            };
        }


        public override IdentityError InvalidRoleName(string role)
        {
            return new IdentityError()
            {
                Code = nameof(InvalidRoleName),
                Description = $"{role} نادرست میباشد  لطفا  نام درست را استفاده کنید  "
            };
        }

        public override IdentityError InvalidUserName(string userName)
        {
            return new IdentityError()
            {
                Code = nameof(InvalidUserName),
                Description = $"{userName} نادرست میباشد  لطفا  نام   کاربری درست را استفاده کنید  "
            };
        }


        public override IdentityError PasswordMismatch()
        {
            return new IdentityError()
            {
                Code = nameof(PasswordMismatch),
                Description = $"  رمز عبور نوشته شده با تاییدی آن مطابقت ندارد لطفا هر دو رمز عبور را یکسان وارد کنید"
            };
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError()
            {
                Code = nameof(PasswordRequiresDigit),
                Description = $"  رمز عبور نوشته شده  ضرورت به ارقام دارد لطفا حد اقل یک رقم را شامل رمز عبور خود بسازید       "
            };
        }


        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError()
            {
                Code = nameof(PasswordRequiresLower),
                Description = $"  رمز عبور نوشته شده  ضرورت به حد اقل یک حرف کوچک انگلیسی دارد"
            };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError()
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = $"  رمز عبور نوشته شده  ضرورت به حد اقل یک سمبول خاص میباشد    "
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError()
            {
                Code = nameof(PasswordTooShort),
                Description = $"  رمز عبور نوشته دارای تعداد حروف کمتر میباشد"
            };
        }
        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError()
            {
                Code = nameof(PasswordRequiresUpper),
                Description = $"  رمز عبور نوشته شده  ضرورت به حد اقل یک حرف بزرگ انگلیسی دارد"
            };
        }
        public override IdentityError UserNotInRole(string role)
        {
            return new IdentityError()
            {
                Code = nameof(UserNotInRole),
                Description = $"کاربر ذیل در این نقش نمیباشد"
            };
        }
        public override IdentityError UserAlreadyInRole(string role)
        {
            return new IdentityError()
            {
                Code = nameof(UserAlreadyInRole),
                Description = $"کاربر ذیل از قبل دارای این حق میباشد"
            };
        }

    }
}
