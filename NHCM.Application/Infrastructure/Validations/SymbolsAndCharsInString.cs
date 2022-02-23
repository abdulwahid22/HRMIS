using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHCM.Application.Infrastructure.Validations
{
   public  class SymbolsAndCharsInString : PropertyValidator
    {


      //  private readonly string _Character;
        private readonly List<string> _ListOfCharacters;

        public SymbolsAndCharsInString(List<string> ListofCharacters/*, string PropertyName*/) : base(string.Empty/*ErrorMessageBuilder(ListofCharacters, PropertyName, null)*/)
        {
            _ListOfCharacters = ListofCharacters;
           // _Character = null;
        }
        //public SymbolsAndCharsInString(string Character, string PropertyName) : base(ErrorMessageBuilder(null, PropertyName, Character))
        //{
        //   // _Character = Character;
        //    _ListOfCharacters = null;
        //}

        protected override bool IsValid(PropertyValidatorContext context)
        {

            bool validity = false;
            //if (_ListOfCharacters != null)
            //{
                foreach (string item in _ListOfCharacters)
                {
                    if (context.PropertyValue.ToString().Contains(item))
                    {
                        validity = false;
                        break;

                    }
                    else
                    {
                        validity = true;
                        continue;

                    }
                }
           // }
            //else
            //{
            //    if (context.PropertyValue.ToString().Contains(_Character))
            //        validity = false;
            //    else
            //        validity = true;
            //}



            return validity;



        }


        // Helper method for this class only
        //private static string ErrorMessageBuilder(List<string> ListOfCharacters, string PropertyName, string Character)
        //{

        //    StringBuilder MessageBuilder = new StringBuilder();


        //    // If Constructor(List<string> ListofCharacters, string PropertyName) is used
        //    if (Character == null)
        //    {
        //        MessageBuilder.Append(PropertyName + "نمی تواند یکی از حروف ذیل را دارا باشد ");

        //        foreach (string character in ListOfCharacters)
        //            MessageBuilder.Append(character).Append("\n");
        //    }

        //    //If Constructor(string Character, string PropertyName) is used
        //    else
        //    {
        //        MessageBuilder.Append(PropertyName)
        //                      .Append(" نمی تواند دارای ")
        //                      .Append(Character)
        //                      .Append(" باشد ");


        //    }

        //    return MessageBuilder.ToString();
        //}



       


    }


    public static class SymbolsAndCharsInStringExtensions
    {
        // Extension methods for application of this validation


        public static IRuleBuilderOptions<T, string> CannotInclude<T>(this IRuleBuilderInitial<T, string> ruleBuilder, List<string> characters/*, string propertyName*/)
        {
            return ruleBuilder.SetValidator(new SymbolsAndCharsInString(characters/*, propertyName*/));
        }

        // CannotInclude Overloaded
        //public static IRuleBuilderOptions<T, string> CannotInclude<T>(this IRuleBuilderInitial<T, string> ruleBuilder, string character, string propertyName)
        //{
        //    return ruleBuilder.SetValidator(new SymbolsAndCharsInString(character, propertyName));
        //}
    }
}
