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
    public class GetProcessConnection : IRequest<List<SearchedProcessConnection>>
    {
        public int? Id { get; set; }
        public int? ModuleId { get; set; }
        public int? ScreenId { get; set; }

    }
    public class GetProcessConnectionHandler : IRequestHandler<GetProcessConnection, List<SearchedProcessConnection>>
    {
        private HCMContext _context;
        public GetProcessConnectionHandler(HCMContext context)
        {
            _context = context;
        }
        public async Task<List<SearchedProcessConnection>> Handle(GetProcessConnection request, CancellationToken cancellationToken)
        {
            List<SearchedProcessConnection> result = new List<SearchedProcessConnection>();
            result = await (from PC in _context.ProcessConnection
                            join P in _context.Process on PC.ProcessId equals P.Id into Processes
                            from ProcessesResults in Processes.DefaultIfEmpty()
                            join C in _context.Process on PC.ConnectionId equals C.Id into Connections
                            from ConnectionResults in Connections.DefaultIfEmpty()
                            join S in _context.Screens on ProcessesResults.ScreenId equals S.Id into Screens
                            from ScreenResults in Screens.DefaultIfEmpty()

                            select new SearchedProcessConnection
                            {
                                Id = PC.Id,
                                ProcessId = PC.ProcessId,
                                ConnectionId = PC.ConnectionId,
                                ProcessText = ProcessesResults.Name,
                                ConnectionText = ConnectionResults.Name,
                                ScreenId = ProcessesResults.ScreenId,
                                ModuleId = (Int16)ScreenResults.ModuleId
                            }).ToListAsync(cancellationToken);
            return result;
        }
    }
}
