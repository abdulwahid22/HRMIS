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
    public class GetLocationQuery : IRequest<List<Location>>
    {
        public int? ID { get; set; }
        public int? TypeID { get; set; }
    }
    public class GetLocationQueryHandler : IRequestHandler<GetLocationQuery, List<Location>>
    {
        private readonly HCMContext _dbContext;
        public GetLocationQueryHandler(HCMContext context)
        {
            _dbContext = context;
        }
        public async Task<List<Location>> Handle(GetLocationQuery request, CancellationToken cancellationToken)
        {

            List<Location> list = new List<Location>();

            if (request.ID != null)
            {
               
                list = await _dbContext.Location.Where(l => l.Id == request.ID).ToListAsync(cancellationToken);
                return list;
            }
            else if(request.TypeID != null)
            {
                // Get location by typeID
                list = await _dbContext.Location.Where(l => l.TypeId == request.TypeID).ToListAsync(cancellationToken);
                return list;
            }
            else 
            {
                
                list = await _dbContext.Location.ToListAsync(cancellationToken);
                return list;

            }



        }
    }
}
