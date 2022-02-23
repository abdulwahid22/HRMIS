using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NHCM.Persistence.Infrastructure.Services
{
    public interface ICurrentUser
    {
        Task<int?> GetUserOrganizationID();
        Task<bool?> IsSuperAdmin();
        Task<int> GetUserId();
    }
}
