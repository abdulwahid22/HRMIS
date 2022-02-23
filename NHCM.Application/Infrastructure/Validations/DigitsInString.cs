using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHCM.Application.Infrastructure.Validations
{
    public class DigitsInString : PropertyValidator
    {

      public DigitsInString(string errorMessage) : base(errorMessage) {

        }
        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (context.PropertyValue.ToString().Any(char.IsDigit))
                return false;
            else
                return true;
           
            
        }
    }

    public static class DigitsInStringExtensions
    {
        public static IRuleBuilderOptions<T, string> NoDigit<T>(this IRuleBuilderInitial<T, string> ruleBuilder )
        {
            return ruleBuilder.SetValidator(new DigitsInString(null));
        }

    }
}
