using MediatR;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NHCM.Application.Lookup.Queries
{
    public class GetPlanTypeQuery : IRequest<List<PlanType>>
    {
        public short? Id { get; set; }
    }

    public class GetPlanTypeQueryHandler : IRequestHandler<GetPlanTypeQuery,List<PlanType>>
    {
        private readonly HCMContext _dbContext;
        public GetPlanTypeQueryHandler(HCMContext context)
        {
            _dbContext = context;
        }

        public async Task<List<PlanType>> Handle(GetPlanTypeQuery request, CancellationToken cancellationToken)
        {
            List<PlanType> list = new List<PlanType>();
            if (request.Id == null || request.Id == default(short))
            {
                list = await _dbContext.PlanType.ToListAsync(cancellationToken);
                return list;
            }
            else
            {
                list = await _dbContext.PlanType.Where(p => p.Id == request.Id).ToListAsync(cancellationToken);
                return list;
            }
        }
    }
}
