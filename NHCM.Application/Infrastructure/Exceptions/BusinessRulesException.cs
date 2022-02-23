using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Infrastructure.Exceptions
{
    public class BusinessRulesException : Exception
    {
        public BusinessRulesException(string Message) : base (Message)
        {

        }
    }
}
