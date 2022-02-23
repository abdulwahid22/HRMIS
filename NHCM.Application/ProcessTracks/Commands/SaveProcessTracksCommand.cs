using MediatR;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NHCM.Application.ProcessTracks.Models;
using NHCM.Application.ProcessTracks.Queries;
using Microsoft.EntityFrameworkCore;

namespace NHCM.Application.ProcessTracks.Commands
{
    public class SaveProcessTracksCommand : IRequest<List<SearchedProcessTracks>>
    {
        public int? Id { get; set; }
        public int RecordId { get; set; }
        public int? ProcessId { get; set; }
        public short ReferedProcessId { get; set; }
        public short StatusId { get; set; }
        public string Remarks { get; set; }
        public int ModuleId { get; set; }

    }


    public class SaveProcessTracksCommandHandler : IRequestHandler<SaveProcessTracksCommand, List<SearchedProcessTracks>>
    {
        private readonly HCMContext _context;
        private readonly IMediator _mediator;
        public SaveProcessTracksCommandHandler(HCMContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<List<SearchedProcessTracks>> Handle(SaveProcessTracksCommand request, CancellationToken cancellationToken)
        {
            List<SearchedProcessTracks> result = new List<SearchedProcessTracks>();
          
            if (request.Id == null)
            {

                    ProcessTracking PT = new ProcessTracking()
                    {
                        RecordId = request.RecordId,
                        ProcessId = (Int16)request.ProcessId,
                        StatusId = 5, // In Process
                        ModuleId = request.ModuleId,
                        ReferedProcessId = 1,
                        CreatedOn = DateTime.Now
                    };
                    _context.ProcessTracking.Add(PT);
                    await _context.SaveChangesAsync(cancellationToken);   
            }
            else
            {
                ProcessTracking track = await (from a in _context.ProcessTracking
                                               where a.Id == request.Id
                                               select a).SingleOrDefaultAsync();
                track.StatusId = 6;
                track.ReferedProcessId = request.ReferedProcessId;
                ProcessTracking PT = new ProcessTracking()
                {
                    RecordId = request.RecordId,
                    ProcessId = (Int16)request.ReferedProcessId,
                    StatusId = 5,
                    Remarks = request.Remarks,
                    ModuleId = request.ModuleId,
                    CreatedOn = DateTime.Now
                };
                _context.ProcessTracking.Add(PT);
                await _context.SaveChangesAsync(cancellationToken);
            }
            result = await _mediator.Send(new SearchProcessTrackQuery()
            {
                RecordId = request.RecordId,
                ModuleId = request.ModuleId
            });
            return result;
        }

    }
}
