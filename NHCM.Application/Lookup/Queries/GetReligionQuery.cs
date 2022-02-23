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
    public class GetReligionQuery : IRequest<List<Religion>>
    {
        public int? ID { get; set; }
    }
    public class GetReligionQueryHandler : IRequestHandler<GetReligionQuery, List<Religion>>
    {
        private readonly HCMContext _dbContext;
        public GetReligionQueryHandler(HCMContext context)
        {
            _dbContext = context;
        }
        public async Task<List<Religion>> Handle(GetReligionQuery request, CancellationToken cancellationToken)
        {




            List<Religion> list = new List<Religion>();

            if (request.ID != null)
            {
                // Return specific language.
                list = await _dbContext.Religion.Where(r => r.Id == request.ID).ToListAsync(cancellationToken);
                return list;
            }
            else
            {
                // Return all languages.
                list = await _dbContext.Religion.ToListAsync(cancellationToken);

                return list;

            }



        }
    }
}
