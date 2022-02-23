using MediatR;
using Microsoft.EntityFrameworkCore;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NHCM.Application.Lookup.Queries
{
    public class GetOrganiztionQuery : IRequest<List<Organization>>
    {
        public int? Id { get; set; }
    }


    public class GetOrganiztionQueryHandler : IRequestHandler<GetOrganiztionQuery, List<Organization>>
    {
        private readonly HCMContext _dbContext;
        public GetOrganiztionQueryHandler(HCMContext context)
        {
            _dbContext = context;
        }

        public async Task<List<Organization>> Handle(GetOrganiztionQuery request, CancellationToken cancellationToken)
        {


            List<Organization> list = new List<Organization>();
            if (request.Id != null)
                list = await _dbContext.Organization.Where(c => c.Id == request.Id).ToListAsync();


            else
                list = await _dbContext.Organization.ToListAsync(cancellationToken);

            return list;
        }
    }
}
