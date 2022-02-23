using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NHCM.Application.Infrastructure.Validations
{
    public class DigitsOnly : PropertyValidator
    {
        public DigitsOnly(string propertyName) : base(propertyName + " تنها عدد بوده میتواند ")
        {

        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (Regex.IsMatch(context.PropertyValue.ToString(), @"^[0-9]$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public static class DigitsOnlyExtensions
    {
        public static IRuleBuilderOptions<T, string> DigitOnly<T>(this IRuleBuilderInitial<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new DariOnly(null));
        }

    }
}
