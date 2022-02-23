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
    public class GetReferenceTypeQuery : IRequest<List<ReferenceType>>
    {
        public short? ID { get; set; }
    }
    public class GetReferenceTypeQueryHandler : IRequestHandler<GetReferenceTypeQuery, List<ReferenceType>>
    {

        private HCMContext _context;

        public GetReferenceTypeQueryHandler(HCMContext context)
        {
            _context = context;
        }
        public async Task<List<ReferenceType>> Handle(GetReferenceTypeQuery request, CancellationToken cancellationToken)
        {

            List<ReferenceType> result = new List<ReferenceType>();

            if (request.ID == null || request.ID == default(short)) result = await _context.ReferenceType.ToListAsync(cancellationToken);
            else result = await _context.ReferenceType.Where(e => e.Id == request.ID).ToListAsync(cancellationToken);


            return result;
        }
    }
}
