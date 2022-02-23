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
    public class GetBloodGroupQuery : IRequest<List<BloodGroup>>
    {
        public int? ID { get; set; }
    }
    public class GetBloodGroupQueryHandler : IRequestHandler<GetBloodGroupQuery, List<BloodGroup>>
    {
        private readonly HCMContext _dbContext;
        public GetBloodGroupQueryHandler(HCMContext context)
        {
            _dbContext = context;
        }
        public async Task<List<BloodGroup>> Handle(GetBloodGroupQuery request, CancellationToken cancellationToken)
        {

            List<BloodGroup> list = new List<BloodGroup>();

            if (request.ID == null || request.ID == 0)
            {

                // Return all languages.
                list = await _dbContext.BloodGroup.ToListAsync(cancellationToken);

                return list;

              
            }
            else
            {
                // Return specific language.
                list = await _dbContext.BloodGroup.Where(b => b.Id == request.ID).ToListAsync(cancellationToken);
                return list;

            }



        }
    }
}
