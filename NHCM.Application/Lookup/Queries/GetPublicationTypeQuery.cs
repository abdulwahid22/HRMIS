using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace NHCM.Application.Lookup.Queries
{
    public class GetPublicationTypeQuery : IRequest<List<PublicationType>>
    {
        public int? ID { get; set; }
    }
    public class GetPublicationTypeQueryHandler : IRequestHandler<GetPublicationTypeQuery, List<PublicationType>>
    {
        private readonly HCMContext _dbContext;
        public GetPublicationTypeQueryHandler(HCMContext context)
        {
            _dbContext = context;
        }
        public async Task<List<PublicationType>> Handle(GetPublicationTypeQuery request, CancellationToken cancellationToken)
        {
            List<PublicationType> list = new List<PublicationType>();

            if (request.ID == null || request.ID == default(int))
            {


                list = await _dbContext.PublicationType.ToListAsync(cancellationToken);

                return list;


            }
            else
            {

                list = await _dbContext.PublicationType.Where(b => b.Id == request.ID).ToListAsync(cancellationToken);
                return list;

            }
        }
    }
}
