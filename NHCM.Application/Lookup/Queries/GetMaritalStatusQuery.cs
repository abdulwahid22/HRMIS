using MediatR;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace NHCM.Application.Lookup.Queries
{
    public class GetMaritalStatusQuery : IRequest<List<MaritalStatus>>
    {
        public int? ID { get; set; }
    }

    public class GetMaritalStatusQueryHandler : IRequestHandler<GetMaritalStatusQuery, List<MaritalStatus>>
    {

        private readonly HCMContext _dbContext;
        public GetMaritalStatusQueryHandler(HCMContext context)
        {
            _dbContext = context;
        }
        public async Task<List<MaritalStatus>> Handle(GetMaritalStatusQuery request, CancellationToken cancellationToken)
        {

            List<MaritalStatus> list = new List<MaritalStatus>();

            if (request.ID != null)
            {
                // Return specific language.
                list = await _dbContext.MaritalStatus.Where(ms => ms.Id == request.ID).ToListAsync(cancellationToken);
                return list;
            }
            else
            {
                // Return all languages.
                list = await _dbContext.MaritalStatus.ToListAsync(cancellationToken);
                return list;

            }


        }
    }
}
