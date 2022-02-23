using MediatR;
using Microsoft.EntityFrameworkCore;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NHCM.Application.Lookup.Queries
{
    public class GetSalaryTypeQuery : IRequest<List<SalaryType>>
    {
        public int? Id { get; set; }
    }

    public class GetSalaryTypeQueryHandler : IRequestHandler<GetSalaryTypeQuery,List<SalaryType>>
    {
        private readonly HCMContext _dbContext;
        public GetSalaryTypeQueryHandler(HCMContext context)
        {
            _dbContext = context;
        }

        public async Task<List<SalaryType>> Handle(GetSalaryTypeQuery request, CancellationToken cancellationToken)
        {
            List<SalaryType> list = new List<SalaryType>();

            if (request.Id == null || request.Id == default(short))
            {


                list = await _dbContext.SalaryType.ToListAsync(cancellationToken);

                return list;


            }
            else
            {

                list = await _dbContext.SalaryType.Where(s => s.Id == request.Id).ToListAsync(cancellationToken);
                return list;

            }
        }
    }
}
