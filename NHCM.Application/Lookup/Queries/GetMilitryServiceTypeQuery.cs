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
    public class GetMilitryServiceTypeQuery : IRequest <List<MilitaryServiceType>>
    {
        public int? Id { get; set; }
    }

    public class GetMilitryServiceTypeQueryHandler : IRequestHandler<GetMilitryServiceTypeQuery, List<MilitaryServiceType>>
    {
        private readonly HCMContext _dbContext;
        public GetMilitryServiceTypeQueryHandler(HCMContext context)
        {
            _dbContext = context;
        }

        public async Task<List<MilitaryServiceType>> Handle(GetMilitryServiceTypeQuery request, CancellationToken cancellationToken)
        {
            List<MilitaryServiceType> list = new List<MilitaryServiceType>();

            if (request.Id == null || request.Id == 0)
            {


                list = await _dbContext.MilitaryServiceType.ToListAsync(cancellationToken);

                return list;


            }
            else
            {

                list = await _dbContext.MilitaryServiceType.Where(b => b.Id == request.Id).ToListAsync(cancellationToken);
                return list;

            }
        }
    }
}
