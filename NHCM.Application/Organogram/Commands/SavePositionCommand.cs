using MediatR;
using Microsoft.EntityFrameworkCore;
using NHCM.Application.Infrastructure.Exceptions;
using NHCM.Application.Organogram.Models;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NHCM.Application.Organogram.Commands
{
    public class SavePositionCommand : IRequest<List<SearchedPosition>>
    {
        public decimal Id { get; set; }
        public int WorkingAreaId { get; set; }
        public int? ParentId { get; set; }
        public int PositionTypeId { get; set; }
        public int LocationId { get; set; }
        public int SalaryTypeId { get; set; }
        public int OrganoGramId { get; set; }
        public short PlanTypeId { get; set; }
        public string Code { get; set; }
    }

    public class SavePositionCommandHandler : IRequestHandler<SavePositionCommand, List<SearchedPosition>>
    {
        private readonly HCMContext _context;
        private readonly IMediator _mediator;
        public SavePositionCommandHandler(HCMContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<List<SearchedPosition>> Handle(SavePositionCommand request, CancellationToken cancellationToken)
        {
            List<SearchedPosition> result = new List<SearchedPosition>();
            List<Position> list = (from a in _context.Position where a.OrganoGramId == request.OrganoGramId && a.WorkingAreaId == request.WorkingAreaId && a.PositionTypeId == request.PositionTypeId && a.Code == request.Code select a).ToList();

            if (list.Any())
            {
                throw new BusinessRulesException("این بست در تشکیل سال انتخاب شده موجود میباشد");
            }
            else
            {
                if (request.Id == default(decimal))
                {
                    String Sorter = "1";
                    if (request.ParentId > 0)
                    {
                        Position parent = (from a in _context.Position where a.Id == request.ParentId select a).SingleOrDefault();
                        String parentsorter = parent.Sorter;
                        int count = (from a in _context.Position where a.ParentId == request.ParentId select a).ToList().Count();
                        Sorter = parentsorter + '.' + (count + 1).ToString();
                    }

                    Position PData = new Position()
                    {
                        WorkingAreaId = request.WorkingAreaId,
                        ParentId = request.ParentId,
                        Code = request.Code,
                        PositionTypeId = request.PositionTypeId,
                        LocationId = request.LocationId,
                        SalaryTypeId = request.SalaryTypeId,
                        Sorter = Sorter,
                        OrganoGramId = request.OrganoGramId,
                        PlanTypeId = request.PlanTypeId,
                    };
                    _context.Position.Add(PData);
                    await _context.SaveChangesAsync(cancellationToken);
                    result = await _mediator.Send(new Queries.SearchPositionQuery() { Id = PData.Id });
                }
                else
                {
                    Position toUpdateRecord = await (from po in _context.Position
                                                     where po.Id == request.Id
                                                     select po).SingleOrDefaultAsync();
                    toUpdateRecord.WorkingAreaId = request.WorkingAreaId;
                    toUpdateRecord.ParentId = request.ParentId;
                    toUpdateRecord.Code = request.Code;
                    toUpdateRecord.PositionTypeId = request.PositionTypeId;
                    toUpdateRecord.LocationId = request.LocationId;
                    toUpdateRecord.SalaryTypeId = request.SalaryTypeId;

                   // toUpdateRecord.Sorter = "4545";
                    if (request.ParentId > 0)
                    {
                        Position parent = (from a in _context.Position where a.Id == request.ParentId select a).SingleOrDefault();
                        String parentsorter = parent.Sorter;
                        int count = (from a in _context.Position where a.ParentId == request.ParentId select a).ToList().Count();
                        toUpdateRecord.Sorter = parentsorter + '.' + (count + 1).ToString();
                    }

                    toUpdateRecord.OrganoGramId = request.OrganoGramId;
                    toUpdateRecord.PlanTypeId = request.PlanTypeId;
                    await _context.SaveChangesAsync(cancellationToken);
                    //result = await _mediator.Send(new Queries.SearchPositionQuery() { Id = toUpdateRecord.ParentId });
                    result = await _mediator.Send(new Queries.SearchPositionQuery() { Id = toUpdateRecord.Id });
                }
            }
            return result;
        }
    }
}
