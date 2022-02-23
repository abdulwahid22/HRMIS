using MediatR;
using Microsoft.EntityFrameworkCore;
using NHCM.Application.Infrastructure.Exceptions;
using NHCM.Application.Lookup.Models;
using NHCM.Application.Lookup.Queries;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NHCM.Application.Lookup.Commands
{
    public class SaveOrgPositionCommand : IRequest<List<SearchedOrgPositionModel>>
    {
        public int Id { get; set; }
        public int PositionTypeId { get; set; }
        public int OrgUnitTypeId { get; set; }
        public int? ParentId { get; set; }
        public short RankId { get; set; }
    }

    public class SaveOrgPositionCommandHandler : IRequestHandler<SaveOrgPositionCommand, List<SearchedOrgPositionModel>>
    {
        private readonly HCMContext _context;
        private readonly IMediator _mediator;
        public SaveOrgPositionCommandHandler(HCMContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;

        }
        public async Task<List<SearchedOrgPositionModel>> Handle(SaveOrgPositionCommand request, CancellationToken cancellationToken)
        {
            List<SearchedOrgPositionModel> result = new List<SearchedOrgPositionModel>();
            if (request.Id == null || request.Id == default(int))
            {
                result = await _mediator.Send(new SearchOrgPosition_Query() { PositionTypeId = request.PositionTypeId, OrgUnitTypeId = request.OrgUnitTypeId });
                if (result.Any())
                {
                    throw new BusinessRulesException("وظیفه انتخاب شده از قبل در سیستم موجود است");
                }

                using (_context) 
                {
                    OrgPosition orgposition = new OrgPosition()
                    {
                        ParentId = request.ParentId,
                        PositionTypeId = request.PositionTypeId,
                        OrgUnitTypeId = request.OrgUnitTypeId,
                        RankId = request.RankId,
                    };
                    _context.OrgPosition.Add(orgposition);
                    await _context.SaveChangesAsync(cancellationToken);

                    result = await _mediator.Send(new SearchOrgPosition_Query() { Id = orgposition.Id });
                }
            }
            else
            {
                using (_context)
                {
                    OrgPosition toUpdateRecord = await _context.OrgPosition.Where(or => or.Id == request.Id).SingleOrDefaultAsync();
                    toUpdateRecord.ParentId = request.ParentId;
                    toUpdateRecord.PositionTypeId = request.PositionTypeId;
                    toUpdateRecord.OrgUnitTypeId = request.OrgUnitTypeId;
                    toUpdateRecord.RankId = request.RankId;
                    await _context.SaveChangesAsync(cancellationToken);
                    result = await _mediator.Send(new SearchOrgPosition_Query() { Id = toUpdateRecord.Id });
                }
            }
            return result;
        }
    }
}
