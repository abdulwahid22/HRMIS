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
   public class GetCertificationQuery : IRequest<List<Certification>>
    {
        public int? ID { get; set; }
    } 
    public class GetCertificationQueryHandler : IRequestHandler<GetCertificationQuery, List<Certification>>
    {
        private readonly HCMContext _dbContext;
        public GetCertificationQueryHandler(HCMContext context)
        {
            _dbContext = context;
        }
        public async Task<List<Certification>> Handle(GetCertificationQuery request, CancellationToken cancellationToken)
        {
            List<Certification> list = new List<Certification>();

            if (request.ID == null || request.ID == 0)
            { 
                list = await _dbContext.Certification.ToListAsync(cancellationToken); 
                return list; 
            }
            else
            { 
                list = await _dbContext.Certification.Where(b => b.Id == request.ID).ToListAsync(cancellationToken);
                return list; 
            }
        }
    }
}
