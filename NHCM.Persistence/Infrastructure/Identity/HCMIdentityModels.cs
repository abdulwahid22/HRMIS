using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Infrastructure.Identity
{
   public class HCMUser : IdentityUser<int>
    {
        public int? OrganizationID { get; set; }
        public bool SuperAdmin { get; set; }
        public bool OrganizationAdmin { get; set; }
        public int EmployeeID { get; set; }
        public bool PasswordChanged { get; set;  }
    }

    public class HCMRole : IdentityRole<int>
    {

    }

    public class HCMUserRole : IdentityUserRole<int>
    {

    }
    public class HCMUserLogin : IdentityUserLogin<int>
    {

    }
    public class HCMUserClaims : IdentityUserClaim<int>
    {

    }

    public class HCMRoleClaim : IdentityRoleClaim<int>
    {

    }
    public class HCMUserToken : IdentityUserToken<int>
    {

    }
  


}
