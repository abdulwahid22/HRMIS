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
    public class GetScreens : IRequest<List<Screens>>
    {
        public int? ID { get; set; }
        public int? ModuleID { get; set; }

    }
    public class GetScreensHandler : IRequestHandler<GetScreens, List<Screens>>
    {
        private HCMContext _context;
        public GetScreensHandler(HCMContext context)
        {
            _context = context;
        }

    
        public async Task<List<Screens>> Handle(GetScreens request, CancellationToken cancellationToken)
        {
            if (request.ModuleID != null || request.ModuleID != 0)
            {
                return await _context.Screens.Where(c => c.ParentId == null && c.ModuleId == request.ModuleID).OrderBy(c => c.Sorter).ToListAsync(cancellationToken);
            }
            else { return await _context.Screens.Where(s => s.Id == request.ID).ToListAsync(cancellationToken); }
        }

       
    }
}
