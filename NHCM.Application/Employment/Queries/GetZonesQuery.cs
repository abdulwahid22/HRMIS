using NHCM.Domain.Entities;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace NHCM.Application.Employment.Queries
{

    public class GetZonesQuery : IRequest<List<Zones>>
    {
        public int? ID { get; set; }
    }

    public class GetZonesQueryHandler : IRequestHandler<GetZonesQuery, List<Zones>>
    {
        private readonly HCMContext _dbContext;
        public GetZonesQueryHandler(HCMContext context)
        {
            _dbContext = context;
        }

        public async Task<List<Zones>> Handle(GetZonesQuery request, CancellationToken cancellationToken)
        {
            List<Zones> zone = new List<Zones>();

            if (request.ID != null)
            {
                // Return specific zone.
                zone = await _dbContext.Zones.Where(d => d.ID == request.ID).ToListAsync(cancellationToken);
                return zone;
            }
            else
            {
                // Return all zone.
                zone = await _dbContext.Zones.ToListAsync(cancellationToken);
                return zone;

            }
        }

      
    }
}
