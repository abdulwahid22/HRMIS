using MediatR;
using Microsoft.EntityFrameworkCore;
using NHCM.Application.Lookup.Models;
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
    public class GetProcess : IRequest<List<SearchedProcess>>
    {
        public int? Id { get; set; }
        public int? ModuleId { get; set; }
        public int? ScreenId { get; set; }

    }
    public class GetProcessHandler : IRequestHandler<GetProcess, List<SearchedProcess>>
    {
        private HCMContext _context;
        public GetProcessHandler(HCMContext context)
        {
            _context = context;
        }
        public async Task<List<SearchedProcess>> Handle(GetProcess request, CancellationToken cancellationToken)
        {
            List<SearchedProcess> result = new List<SearchedProcess>();
            if (request.ScreenId != null)
            {
                result = await (from P in _context.Process
                                join S in _context.Screens on P.ScreenId equals S.Id into Screens
                                from ScreenResults in Screens.DefaultIfEmpty()
                                join M in _context.Module on ScreenResults.ModuleId equals M.Id into Modules
                                from ModuleResult in Modules.DefaultIfEmpty()
                                where P.ScreenId == request.ScreenId
                                select new SearchedProcess
                                {
                                    Id = P.Id,
                                    Name = P.Name,
                                    Description = P.Description,
                                     ScreenId = P.ScreenId,
                                    ModuleId = ModuleResult.Id, 
                                    
                                }).ToListAsync(cancellationToken);
            }
            return result;
        }
    }
}
