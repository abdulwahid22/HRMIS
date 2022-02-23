using FluentValidation;
using NHCM.Application.Accounts.Queries;
namespace NHCM.Application.Accounts.Validators
{
   public class GetUsersQueryValidators : AbstractValidator<GetUsersQuery>
    {
        public GetUsersQueryValidators()
        {
            // Validates the two properties. Throws validation exception if there is not at least on search term.
            // To succeed this validation user must provide a value either for EmployeeID or UserName.
            RuleFor(x => x.UserName).NotEmpty()
                                    .When(x => x.EmployeeID is null)
                                    .WithMessage("لطفا حداقل یک ترم را برای جستجو انتخاب نمایید");
            RuleFor(x => x.EmployeeID).NotNull()
                                      .When(x => string.IsNullOrEmpty(x.UserName))
                                      .WithMessage("لطفا حداقل یک ترم را برای جستجو انتخاب نمایید");
        }
    }
}
