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
  



        public class GetJobStatusQuery : IRequest<List<JobStatus>>
        {
            public int? ID { get; set; }
        }
        public class GetJobStatusQueryHandler : IRequestHandler<GetJobStatusQuery, List<JobStatus>>
        {
            private readonly HCMContext _dbContext;
            public GetJobStatusQueryHandler(HCMContext context)
            {
                _dbContext = context;
            }
            public async Task<List<JobStatus>> Handle(GetJobStatusQuery request, CancellationToken cancellationToken)
            {

            List<JobStatus> list = new List<JobStatus>();

                if (request.ID == null || request.ID == 0)
                {

                    // Return all languages.
                    list = await _dbContext.JobStatus.ToListAsync(cancellationToken);

                    return list;


                }
                else
                {
                    // Return specific language.
                    list = await _dbContext.JobStatus.Where(b => b.Id == request.ID).ToListAsync(cancellationToken);
                    return list;

                }



            }
        }
    }

