using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using Microsoft.EntityFrameworkCore;

namespace NHCM.Application.Lookup.Queries
{
   public class GetOrganizationTypeQuery :IRequest<List<OrganizationType>>
    {
        public int? ID { get; set; }
    }

    public class GetOrganizationTypeQueryHandler : IRequestHandler<GetOrganizationTypeQuery, List<OrganizationType>>
    {
        private HCMContext _context;
        public GetOrganizationTypeQueryHandler(HCMContext context)
        {
            _context = context;
        }

        public async Task<List<OrganizationType>> Handle(GetOrganizationTypeQuery request, CancellationToken cancellationToken)
        {
            List<OrganizationType> result = new List<OrganizationType>();

            if (request.ID == null || request.ID == default(int)) result = await _context.OrganizationType.ToListAsync(cancellationToken);
            
            else result = await _context.OrganizationType.Where(e => e.Id == request.ID).ToListAsync(cancellationToken);


            return result;
        }
    }
}
