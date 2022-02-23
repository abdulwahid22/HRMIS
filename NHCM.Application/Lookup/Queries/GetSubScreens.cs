using MediatR;
using Microsoft.EntityFrameworkCore;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NHCM.Application.Lookup.Queries
{
    public class GetSubScreens : IRequest<List<Screens>>
    {
        public int ID { get; set; }
    }

    public class GetScreensByParentIDHandler : IRequestHandler<GetSubScreens, List<Screens>>
    {
        private HCMContext _context;
        public GetScreensByParentIDHandler(HCMContext context)
        {
            _context = context;
        }
        

        public async Task<List<Screens>> Handle(GetSubScreens request, CancellationToken cancellationToken)
        {
            return await _context.Screens.Where(s => s.ParentId == request.ID).OrderBy(c=>c.Sorter).ToListAsync(cancellationToken);
        }
    }

}
