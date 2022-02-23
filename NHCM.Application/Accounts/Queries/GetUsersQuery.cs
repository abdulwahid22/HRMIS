using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NHCM.Application.Accounts.Models;
using NHCM.Persistence.Identity.Infrastructure;
using NHCM.Persistence.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace NHCM.Application.Accounts.Queries
{
    public class GetUsersQuery : IRequest<List<SearchedUsersDTO>>
    {
        public string UserName { get; set; }
        public int? EmployeeID { get; set; }

    }
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<SearchedUsersDTO>>
    {
        private readonly HCMIdentityDbContext _dbContext;
        public GetUsersQueryHandler(HCMIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<SearchedUsersDTO>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            List<SearchedUsersDTO> result = new List<SearchedUsersDTO>();
            List<HCMUser> LisOfUsers = new List<HCMUser>();

            if (request.EmployeeID !=null && request.EmployeeID != default(int))
            {
                LisOfUsers = await (from u in _dbContext.Users
                                                  where u.EmployeeID == request.EmployeeID
                                                  select u).ToListAsync(cancellationToken);
            }
            else
            {
                LisOfUsers = await (from u in _dbContext.Users
                                    where u.UserName.Contains(request.UserName)
                                    select u).ToListAsync(cancellationToken);
            }

            return result;
        }
    }
}
