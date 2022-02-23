using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NHCM.Application.Infrastructure.Validations
{
    public class MobileNumber : PropertyValidator
    {
        public MobileNumber(string propertyName) : base(propertyName + " نمبر موبایل درست نیست ")
        {

        }
        protected override bool IsValid(PropertyValidatorContext context)
        {

            if (Regex.IsMatch(context.PropertyValue.ToString(), @"^[0-9]{10}$"))
            {
                return true;
            }
            else
            {
                return false;
            }
            // throw new NotImplementedException();
        }
    }

    public static class MobileNumberExtensions
    {
        public static IRuleBuilderOptions<T, string> MobileNumberOnly<T>(this IRuleBuilderInitial<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new MobileNumber(null));
        }

    }
}
