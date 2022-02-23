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
 


    public class GetExperienceTypeQuery : IRequest<List<ExperienceType>>
    {
        public int? ID { get; set; }
    }
    public class GetExperienceTypeQueryHandler : IRequestHandler<GetExperienceTypeQuery, List<ExperienceType>>
    {
        private readonly HCMContext _dbContext;
        public GetExperienceTypeQueryHandler(HCMContext context)
        {
            _dbContext = context;
        }
        public async Task<List<ExperienceType>> Handle(GetExperienceTypeQuery request, CancellationToken cancellationToken)
        {

            List<ExperienceType> list = new List<ExperienceType>();

            if (request.ID == null || request.ID == 0)
            {

                
                list = await _dbContext.ExperienceType.ToListAsync(cancellationToken);

                return list;


            }
            else
            {
                
                list = await _dbContext.ExperienceType.Where(b => b.Id == request.ID).ToListAsync(cancellationToken);
                return list;

            }



        }
    }
}
