using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NHCM.Persistence.Infrastructure.Identity.Policies
{
   public  class NewlyRegisteredUsers : IAuthorizationRequirement
    {

        public bool _PasswordMustNotBeChanged { get; set; }

        public NewlyRegisteredUsers(bool PasswordMustNotBeChanged)
        {
            _PasswordMustNotBeChanged = PasswordMustNotBeChanged;
        }


    }

    public class NewlyRegisteredUsersHandler : AuthorizationHandler<NewlyRegisteredUsers>
    {
        private readonly UserManager<HCMUser> _userManager;
        public NewlyRegisteredUsersHandler(UserManager<HCMUser> userManager)
        {
            _userManager = userManager;
        }
        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, NewlyRegisteredUsers requirement)
        {
            HCMUser currentUser = await _userManager.FindByNameAsync(context.User.Identity.Name);

            if (currentUser.PasswordChanged == requirement._PasswordMustNotBeChanged)
            {
                context.Fail();
            }

            else
            {
                context.Succeed(requirement);
            }
        }
    }
}
