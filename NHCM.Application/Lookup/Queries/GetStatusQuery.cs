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
    public class GetStatusQuery :IRequest<List<NHCM.Domain.Entities.Status>>
    {
        public string category { get; set; }
    }

    public class GetStatusQueryHandler : IRequestHandler<GetStatusQuery, List<NHCM.Domain.Entities.Status>>
    {
        private readonly HCMContext _DbContext;
        public GetStatusQueryHandler(HCMContext context)
        {
            _DbContext = context;
        }

        public async Task<List<NHCM.Domain.Entities.Status>> Handle(GetStatusQuery request, CancellationToken cancellationToken)
        {
            List<NHCM.Domain.Entities.Status> list = new List<NHCM.Domain.Entities.Status>();

            if (request.category==null || request.category==string.Empty)
            {
                // Return all categories of Status
                list = await _DbContext.Status.ToListAsync(cancellationToken);
                return list;
            }

            else
            {
                //Return Specific Category
                list = await _DbContext.Status.Where(s=> s.Category==request.category).ToListAsync(cancellationToken);
                return list;
            }

        }
    }
}
