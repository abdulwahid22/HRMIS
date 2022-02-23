using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using NHCM.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using NHCM.Persistence;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace NHCM.Application.Lookup.Queries
{
    public class GetGenderQuery : IRequest<List<Gender>>
    {
        public int? ID { get; set; }
    }

    public class GetGenderQueryHandler : IRequestHandler<GetGenderQuery, List<Gender>>
    {
        private readonly HCMContext _dbContext;
        public GetGenderQueryHandler(HCMContext context)
        {
            _dbContext = context;
        }
        public async Task<List<Gender>> Handle(GetGenderQuery request, CancellationToken cancellationToken)
        {
            List<Gender> genders = new List<Gender>();

            if (request.ID != null)
            {
                genders = await _dbContext.Gender.Where(g => g.ID == request.ID).ToListAsync(cancellationToken);
                return genders;
            }
            else
            {
                genders = await _dbContext.Gender.ToListAsync(cancellationToken);
                return genders;

            }


        }
    }
}
