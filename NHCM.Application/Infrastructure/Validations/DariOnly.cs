using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NHCM.Application.Infrastructure.Validations
{
    public class DariOnly : PropertyValidator
    {
        public DariOnly(string propertyName) : base (propertyName + " تنها به زبان دری بوده میتواند ")
        {

        }
        protected override bool IsValid(PropertyValidatorContext context)
        {

           if(Regex.IsMatch(context.PropertyValue.ToString(), @"[\u0600-\u06FF]"))
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

    public static class DariOnlyExtensions
    {
        public static IRuleBuilderOptions<T, string> DariTextOnly<T>(this IRuleBuilderInitial<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new DariOnly(null));
        }

    }
}
