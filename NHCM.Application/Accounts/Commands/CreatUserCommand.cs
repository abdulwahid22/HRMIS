using MediatR;
using Microsoft.AspNetCore.Identity;
using NHCM.Application.Infrastructure.Exceptions;
using NHCM.Persistence.Infrastructure.Identity;
using NHCM.Persistence.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NHCM.Application.Accounts.Commands
{
    public class CreateUserCommand : IRequest<List<string>>
    {

        public string UserName { get; set; }
        public string Email { get; set; }
        public int? OrganizationID { get; set; }
        public int EmployeeID { get; set; }
        public bool PasswordChanged { get; set; }


       
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, List<string>>
    {
        
        private readonly UserManager<HCMUser> _userManager;
        
        public CreateUserCommandHandler(SignInManager<HCMUser> signInManager, UserManager<HCMUser> userManager, ICurrentUser currentUser)
        {
            _userManager = userManager;
            
        }
        public async Task<List<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Final Result
            List<string> fresult = new List<string>();

            string GeneratedPassword = CredentialHelper.GenerateRandomPassowrd(CredentialHelper.SystemPasswordPolicy);
            HCMUser user = new HCMUser()
            {

                UserName =request. UserName,
                Email = request.Email,
                OrganizationID = request.OrganizationID,
                EmployeeID = request.EmployeeID,
                PasswordChanged = request.PasswordChanged
            };


            IdentityResult result = await _userManager.CreateAsync(user, GeneratedPassword);

            if (result.Succeeded)
            {
                fresult.Add(GeneratedPassword);
                
            }
            else
            {
                StringBuilder ErrorBuilder = new StringBuilder();
                foreach(IdentityError error in result.Errors)
                    ErrorBuilder.Append(error.Description).Append("\n");

                throw new BusinessRulesException(ErrorBuilder.ToString());
            }

            return fresult;
        }
    }
}
