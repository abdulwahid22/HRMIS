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
    public class GetDistrictQuery : IRequest<List<District>>
    {
        public int? ID { get; set; }
    }
    public class GetDistrictQueryHandler : IRequestHandler<GetDistrictQuery, List<District>>
    {
        private HCMContext _context;

        public GetDistrictQueryHandler(HCMContext context)
        {
            _context = context;
        }
        public async Task<List<District>> Handle(GetDistrictQuery request, CancellationToken cancellationToken)
        {  
            List<District> result = new List<District>();

            if (request.ID == null || request.ID == default(int)) result = await _context.District.ToListAsync(cancellationToken);
            else result = await _context.District.Where(e => e.Id == request.ID).ToListAsync(cancellationToken);            
            return result;
        }
    }
}
