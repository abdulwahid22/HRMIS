using MediatR;
using System;
using System.Collections.Generic;
using NHCM.Domain.Entities;
using System.Text;
using NHCM.Persistence;
using Microsoft.EntityFrameworkCore;

using System.Linq;

using System.Threading;
using System.Threading.Tasks;

namespace NHCM.Application.Lookup.Queries
{
    public class GetEthnicityQuery : IRequest<List<Ethnicity>>
    {
        public int? ID { get; set; }
    }    
    public class GetEthnicityQueryHandler : IRequestHandler<GetEthnicityQuery, List<Ethnicity>>
    {
        private readonly HCMContext _dbContext;
        public GetEthnicityQueryHandler(HCMContext context)
        {
            _dbContext = context;
        }
        public async Task<List<Ethnicity>> Handle(GetEthnicityQuery request, CancellationToken cancellationToken)
        {
            List<Ethnicity> ethnicities = new List<Ethnicity>();
            if (request.ID != null)
            {
                // Return specific ethnicity.
                ethnicities = await _dbContext.Ethnicity.Where(l => l.Id == request.ID).ToListAsync(cancellationToken);
                return ethnicities;
            }
            else
            {
                // Return all ethnicity.
                ethnicities = await _dbContext.Ethnicity.ToListAsync(cancellationToken);
                return ethnicities;
            }
        }
    }
}
