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
  
    public class GetInstituteQuery : IRequest<List<Institute>>
    {
        public int? ID { get; set; }
    }


    public class GetInstituteQueryHandler : IRequestHandler<GetInstituteQuery, List<Institute>>
    {
        private readonly HCMContext _dbContext;
        public GetInstituteQueryHandler(HCMContext context)
        {
            _dbContext = context;
        }
      
        public async Task<List<Institute>> Handle(GetInstituteQuery request, CancellationToken cancellationToken)
        {
            List<Institute> institutes = new List<Institute>();

            if (request.ID != null)
            {

                institutes = await _dbContext.Institute.Where(i => i.Id == request.ID).ToListAsync(cancellationToken);
                return institutes;
            }
            else
            {

                institutes = await _dbContext.Institute.ToListAsync(cancellationToken);
                return institutes;

            }
        }
    }
}
