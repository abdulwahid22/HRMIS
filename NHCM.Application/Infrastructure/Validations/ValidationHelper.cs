using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Infrastructure.Validations
{
    public class ValidationHelper
    {
        public static List<string> ForbiddenSymbols = new List<string> { "$", ".", "%", "*", "!", "@", "#", "%", "^", "&", "(", ")", "~", "-", "+","٪" };
        public static List<char> EnglishCapitalLetters = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        public static List<char> EnglishSmallLetters = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };


        public static string StringyfyList (List<string> input)
        {
            StringBuilder builder = new StringBuilder();
            
            foreach(string str in input)
                builder.Append(str).Append(" ").Append(", ");

            return builder.ToString();
        }
    }
}
