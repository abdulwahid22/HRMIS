
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NHCM.Persistence.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NHCM.Persistence.Infrastructure.Services
{
    public class CurrentUser : ICurrentUser
    {
      
        UserManager<HCMUser> _userManager;
        IHttpContextAccessor _httpContextAccessor;

        public CurrentUser( UserManager<HCMUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int?> GetUserOrganizationID()
        {

            HCMUser user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            int? CurrentUserOrganizationID = user.OrganizationID;

            return CurrentUserOrganizationID ?? 0;
            
        }

        public async Task<bool?> IsSuperAdmin()
        {
            HCMUser user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            bool? IsUserASuperAdmin = user.SuperAdmin;
            return IsUserASuperAdmin ?? false;
        }

        public async Task<int> GetUserId()
        {
            HCMUser user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            int UserID= user.Id;
            return UserID;
           
        }

  
    }
}
