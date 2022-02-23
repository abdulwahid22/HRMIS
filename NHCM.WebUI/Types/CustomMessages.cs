using System;
using NHCM.Application.Infrastructure.Exceptions;
using System.Text;

namespace NHCM.WebUI.Types
{
    public class CustomMessages
    {
        public static string InternalSystemException { get; } = "کاربر محترم درخواست شما مواجه با مشکل تخنیکی میباشد. لطفا با مسول سیستم به تماس شوید";
        public static string ValidationExceptionTitle { get; } = "اطلاعات فورم درست نمیباشد";
        public static string BusinessRuleExceptionTitle { get; } = "کوشش خلاف اصول";

        public static string StateExceptionTitle(Exception ex)
        {
            Type exceptionType = ex.GetType();
            StringBuilder ExceptionTitleBuilder = new StringBuilder();
            if (exceptionType.Equals(typeof(ValidationException)))
            {
                ExceptionTitleBuilder.Append(ValidationExceptionTitle);
            }
            else if (exceptionType.Equals(typeof(BusinessRulesException)))
            {
                ExceptionTitleBuilder.Append(BusinessRuleExceptionTitle);
            }
            else
            {
                ExceptionTitleBuilder.Append(InternalSystemException);
            }
            return ExceptionTitleBuilder.ToString();
        }


        public static string DescribeException(Exception ex)
        {
            bool ShowStackTrace = false;
            StringBuilder DescriptionBuilder = new StringBuilder();
            ShowStackTrace = Convert.ToBoolean(ConfigurationProvider._ShowStackTrace);

            if (ShowStackTrace)
            {

                DescriptionBuilder
                             .Append("Message: ")
                             .Append("\n")
                             .Append(ex.Message)
                             .Append("Inner Exception:")
                             .Append("\n")
                             .Append(ex.InnerException != null ? ex.InnerException.ToString() : string.Empty)
                             .Append("Stack Trace:")
                             .Append("\n")
                             .Append(ex.StackTrace);
            }
            else
            {
                DescriptionBuilder.Append("\n").Append(ex.Message);
            }

            return DescriptionBuilder.ToString();

        }

        public static UIResult FabricateException(Exception ex)
        {
            return new UIResult()
            {
                Data = null,
                Status = UIStatus.Failure,
                Text = StateExceptionTitle(ex),
                Description = DescribeException(ex)
            };
        }
    }
}
