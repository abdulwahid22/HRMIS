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
    public class GetSkillTypeQuery : IRequest<List<SkillType>>
    {

        public int? ID { get; set; }
    }


    public class GetSkillTypeQueryHandler : IRequestHandler<GetSkillTypeQuery, List<SkillType>>
    {
        private readonly HCMContext _dbContext;
        public GetSkillTypeQueryHandler(HCMContext context)
        {
            _dbContext = context;
        }
        public async Task<List<SkillType>> Handle(GetSkillTypeQuery request, CancellationToken cancellationToken)
        {
            List<SkillType> list = new List<SkillType>();

            if (request.ID == null || request.ID == 0)
            {

                // Return all languages.
                list = await _dbContext.SkillType.ToListAsync(cancellationToken);

                return list;


            }
            else
            {
                // Return specific language.
                list = await _dbContext.SkillType.Where(b => b.Id == request.ID).ToListAsync(cancellationToken);
                return list;

            }
        }
    }
}
