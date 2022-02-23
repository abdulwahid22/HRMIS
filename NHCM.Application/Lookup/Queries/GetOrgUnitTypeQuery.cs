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
    public class GetOrgUnitTypeQuery : IRequest<List<OrgUnitType>>
    {
        public int? ID { get; set; }
    }

    public class GetOrgUnitTypeQueryHandler : IRequestHandler<GetOrgUnitTypeQuery, List<OrgUnitType>>
    { 
        private readonly HCMContext _dbContext;
        public GetOrgUnitTypeQueryHandler(HCMContext context)
        {
            _dbContext = context;
        }
        public async Task<List<OrgUnitType>> Handle(GetOrgUnitTypeQuery request, CancellationToken cancellationToken)
        {
            List<OrgUnitType> list = new List<OrgUnitType>();

            if (request.ID != null || request.ID == 0)
            {
                list = await _dbContext.OrgUnitType.Where(orgt => orgt.Id == request.ID).ToListAsync(cancellationToken);         
                return list;
            }
            else
            {
                list = await _dbContext.OrgUnitType.ToListAsync(cancellationToken);
                return list;
            }
        }
    }
}
