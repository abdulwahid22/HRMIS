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


    public class GetEducationLevelQuery : IRequest<List<EducationLevel>>
    {
        public int? ID { get; set; }
    }
    public class GetEducationLevelQueryHandler : IRequestHandler<GetEducationLevelQuery, List<EducationLevel>>
    {
        private readonly HCMContext _dbContext;
        public GetEducationLevelQueryHandler(HCMContext context)
        {
            _dbContext = context;
        }
        public async Task<List<EducationLevel>> Handle(GetEducationLevelQuery request, CancellationToken cancellationToken)
        {

            List<EducationLevel> list = new List<EducationLevel>();

            if (request.ID == null || request.ID == 0)
            {

                // Return all languages.
                list = await _dbContext.EducationLevel.ToListAsync(cancellationToken);

                return list;


            }
            else
            {
                // Return specific language.
                list = await _dbContext.EducationLevel.Where(b => b.Id == request.ID).ToListAsync(cancellationToken);
                return list;

            }



        }
    }
}
