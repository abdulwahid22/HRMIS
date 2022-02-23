using NHCM.Domain.Entities;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace NHCM.Application.Employment.Queries
{

    public class GetPersonQuery : IRequest<List<Person>>
    {
        public int? ID { get; set; }
    }

    public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, List<Person>>
    {
        private readonly HCMContext _dbContext;
        public GetPersonQueryHandler(HCMContext context)
        {
            _dbContext = context;
        }

        public async Task<List<Person>> Handle(GetPersonQuery request, CancellationToken cancellationToken)
        {
            List<Person> persons = new List<Person>();

            if (request.ID != null)
            {
                // Return specific person.
                persons = await _dbContext.Person.Take(10).Where(d => d.Id == request.ID).ToListAsync(cancellationToken);
                return persons;
            }
            else
            {
                // Return all person.
                persons = await _dbContext.Person.Take(10).ToListAsync(cancellationToken);
                return persons;

            }
        }

      
    }
}
