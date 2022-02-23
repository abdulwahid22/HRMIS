using MediatR;
using Microsoft.EntityFrameworkCore;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NHCM.Application.Lookup.Queries
{
    public class GetPositionTypeQuery : IRequest<List<PositionType>>
    {
        public short? Id { get; set; }
    }

    public class GetPositionTypeQueryHandler : IRequestHandler<GetPositionTypeQuery, List<PositionType>>
    {
        private readonly HCMContext _dbContext;
        public GetPositionTypeQueryHandler(HCMContext context)
        {
            _dbContext = context;
        }
        public async Task<List<PositionType>> Handle(GetPositionTypeQuery request, CancellationToken cancellationToken)
        {
            List<PositionType> list = new List<PositionType>();

            if (request.Id == null || request.Id == 0)
            {


                list = await _dbContext.PositionType.ToListAsync(cancellationToken);

                return list;


            }
            else
            {

                list = await _dbContext.PositionType.Where(b => b.Id == request.Id).ToListAsync(cancellationToken);
                return list;

            }
        }
    }
}
