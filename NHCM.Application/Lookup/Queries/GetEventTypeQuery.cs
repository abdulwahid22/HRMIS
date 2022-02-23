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
    public class GetEventTypeQuery : IRequest<List<EventType>>
    {
        public int? ID { get; set; }
        public int? ScreenID { get; set; }
    }
    public class GetEventTypeQueryHandler : IRequestHandler<GetEventTypeQuery, List<EventType>>
    {
        private readonly HCMContext _context;
        public GetEventTypeQueryHandler(HCMContext context)
        {
            _context = context;
        }
        public async Task<List<EventType>> Handle(GetEventTypeQuery request, CancellationToken cancellationToken)
        {
            List<EventType> result = new List<EventType>();
            result = await _context.EventType.Where(a=>a.ParentId == 2).ToListAsync(cancellationToken);
            return result;
        }
    }
}
