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
    public class GetOrgPositionTypeQuery : IRequest<List<PositionType>>
    {
        public int? ID { get; set; }
    }

    public class GetOrgPositionTypeQueryHandler : IRequestHandler<GetOrgPositionTypeQuery, List<PositionType>>
    {
        private readonly HCMContext _dbcontext;
        public GetOrgPositionTypeQueryHandler(HCMContext context)
        {
            _dbcontext = context;
        }
        public async Task<List<PositionType>> Handle(GetOrgPositionTypeQuery request, CancellationToken cancellationToken)
        {
            List<PositionType> result = new List<PositionType>(); 
            if (request.ID == null || request.ID == default(int)) result = await _dbcontext.PositionType.ToListAsync(cancellationToken);
            else result = await _dbcontext.PositionType.Where(e => e.Id == request.ID).ToListAsync(cancellationToken);
            return result;
        }
    }
}
