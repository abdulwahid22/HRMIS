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
    public class GetRelationshipQuery : IRequest<List<Relationship>>
    {
        public int? ID { get; set; }

    }

    public class GetRelationshipQueryHandler : IRequestHandler<GetRelationshipQuery, List<Relationship>>
    {
        private readonly HCMContext _dbContext;
        public GetRelationshipQueryHandler(HCMContext context)
        {
            _dbContext = context;
        }
        public async Task<List<Relationship>> Handle(GetRelationshipQuery request, CancellationToken cancellationToken)
        {
            List<Relationship> list = new List<Relationship>();

            if (request.ID == null || request.ID == 0)
            {

                // Return all languages.
                list = await _dbContext.Relationship.ToListAsync(cancellationToken);

                return list;


            }
            else
            {
                // Return specific language.
                list = await _dbContext.Relationship.Where(b => b.Id == request.ID).ToListAsync(cancellationToken);
                return list;

            }

        }
    }
}
