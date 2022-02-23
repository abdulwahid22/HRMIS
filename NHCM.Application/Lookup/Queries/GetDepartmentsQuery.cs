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
    public class GetDepartmentsQuery : IRequest<List<Department>>
    {
        public int? ID { get; set; }
    }

    public class GetDepartmentsQueryHandler : IRequestHandler<GetDepartmentsQuery, List<Department>>
    {
        private readonly HCMContext _dbContext;
        public GetDepartmentsQueryHandler(HCMContext context)
        {
            _dbContext = context;
        }

        public async Task<List<Department>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
        {
            List<Department>  departments = new List<Department>();

            if (request.ID != null)
            {
                // Return specific language.
                departments = await _dbContext.Department.Where(d => d.Id == request.ID).ToListAsync(cancellationToken);
                return departments;
            }
            else
            {
                // Return all languages.
                departments = await _dbContext.Department.ToListAsync(cancellationToken);
                return departments;

            }
        }
    }
}
