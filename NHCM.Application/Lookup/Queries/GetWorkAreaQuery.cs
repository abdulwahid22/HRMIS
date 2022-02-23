using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using Microsoft.EntityFrameworkCore;

namespace NHCM.Application.Lookup.Queries
{
    public class GetWorkAreaQuery : IRequest<List<WorkArea>>
    {
        public int? ID { get; set; }
    }

    public class GetWorkAreaQueryHandler : IRequestHandler<GetWorkAreaQuery, List<WorkArea>>
    {
        private readonly HCMContext _dbContext;
        public GetWorkAreaQueryHandler(HCMContext context)
        {
            _dbContext = context;
        }

        public async Task<List<WorkArea>> Handle(GetWorkAreaQuery request, CancellationToken cancellationToken)
        {
            List<WorkArea> resultlist = new List<WorkArea>();

            if (request.ID != null)
            {  
                resultlist = await _dbContext.WorkArea.Where(l => l.Id == request.ID).ToListAsync(cancellationToken);
                return resultlist;
            }
            else
            {
                resultlist = await _dbContext.WorkArea.ToListAsync(cancellationToken);
                return resultlist;
            }
        }
    }
}
