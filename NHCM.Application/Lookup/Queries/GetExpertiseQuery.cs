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
   public class GetExpertiseQuery : IRequest<List<Expertise>>
    {
        public int? ID { get; set; }
    }

    public class GetExpertiseQueryHandler : IRequestHandler<GetExpertiseQuery, List<Expertise>>
    {

        private HCMContext _context;

        public GetExpertiseQueryHandler(HCMContext context)
        {
            _context = context;
        }
        public async Task<List<Expertise>> Handle(GetExpertiseQuery request, CancellationToken cancellationToken)
        {




            List<Expertise> result = new List<Expertise>();

            if (request.ID == null || request.ID == default(int)) result = await _context.Expertise.ToListAsync(cancellationToken);
            else result = await _context.Expertise.Where(e => e.Id == request.ID).ToListAsync(cancellationToken);


            return result;

        }
    }
}
