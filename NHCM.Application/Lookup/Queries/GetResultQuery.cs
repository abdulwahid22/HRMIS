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
    public class GetResultQuery  : IRequest<List<Result>>
    {
        public int? ID { get; set; }
    }

    public class GetResultQueryHandler : IRequestHandler<GetResultQuery, List<Result>>
    {

        private readonly HCMContext _dbContext;
        public GetResultQueryHandler(HCMContext context)
        {
            _dbContext = context;
        }
        public async Task<List<Result>> Handle(GetResultQuery request, CancellationToken cancellationToken)
        {
            List<Result> list = new List<Result>();

            if (request.ID == null || request.ID == 0)
            {

               
                list = await _dbContext.Result.ToListAsync(cancellationToken);

                return list;


            }
            else
            {
                
                list = await _dbContext.Result.Where(b => b.Id == request.ID).ToListAsync(cancellationToken);
                return list;

            }
        }
    }
}
