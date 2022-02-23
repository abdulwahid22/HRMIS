using MediatR;
using NHCM.Application.Organogram.Models;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NHCM.Application.ProcessTracks.Models;
using Microsoft.EntityFrameworkCore;

namespace NHCM.Application.ProcessTracks.Queries
{
    public class SearchProcessTrackQuery : IRequest<List<SearchedProcessTracks>>
    {
        public int RecordId { get; set; }
        public int ModuleId { get; set; }
    }


    public class SearchProcessTrackQueryHandler : IRequestHandler<SearchProcessTrackQuery, List<SearchedProcessTracks>>
    {
        private HCMContext _context;
        public SearchProcessTrackQueryHandler(HCMContext context) { _context = context; }
        public async Task<List<SearchedProcessTracks>> Handle(SearchProcessTrackQuery request, CancellationToken cancellationToken)
        {
            List<SearchedProcessTracks> result = new List<SearchedProcessTracks>();
            result = await (from PT in _context.ProcessTracking
                            join PR in _context.Process on PT.ProcessId equals PR.Id into Processes
                            from ProcessResult in Processes.DefaultIfEmpty()
                            join M in _context.Module on PT.ModuleId equals M.Id into Modules
                            from ModuleResult in Modules.DefaultIfEmpty()
                            join S in _context.Status on PT.StatusId equals S.Id into Status
                            from StatusResult in Status.DefaultIfEmpty()
                            where request.RecordId == PT.RecordId && request.ModuleId == PT.ModuleId
                            select new SearchedProcessTracks
                            {
                                Id = PT.Id,
                                RecordId = PT.RecordId,
                                ModuleId = PT.ModuleId,
                                ProcessId = PT.ProcessId,
                                StatusId = PT.StatusId,
                                Remarks = PT.Remarks,
                                ReferedProcessId = PT.ReferedProcessId,
                                ProcessText = ProcessResult.Name,
                                ModuleText = ModuleResult.Name,
                                CreatedOn = PT.CreatedOn,
                                StatusText = StatusResult.Dari,
                                DateText = PersianLibrary.PersianDate.GetFormatedString(PT.CreatedOn)
                            }).OrderByDescending(c=>c.CreatedOn).ToListAsync(cancellationToken);
            return result;
        }
    }
}
