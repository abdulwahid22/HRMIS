using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHCM.Application.Infrastructure.Exceptions
{
    public class ValidationException : Exception
    {
        //public ValidationException()
        //    : base("Validation Exception occured in Application layer")
        //{

        //}



        public ValidationException(List<ValidationFailure> failures)
            : base(ErrorMessageBuilder(failures))
        {


            Failures = new Dictionary<string, string[]>();

            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();





                Failures.Add(propertyName, propertyFailures);

            }


        }

        public IDictionary<string, string[]> Failures { get; }



        private static string ErrorMessageBuilder(List<ValidationFailure> failures)
        {
            string Message = string.Empty;
            foreach (ValidationFailure vf in failures)
            {
                Message +="\n" + vf.ErrorMessage;
            }
            return Message;
        }
    }
}
