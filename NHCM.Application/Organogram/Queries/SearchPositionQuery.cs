using MediatR;
using NHCM.Application.Organogram.Models;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NHCM.Application.Organogram.Queries
{
    public class SearchPositionQuery : IRequest<List<SearchedPosition>>
    { 
        public decimal? Id { get; set; }
        public int? OrganoGramId { get; set; }
        public int? ParentId { get; set; }
        public short? PositionTypeId { get; set; }
        public short? RankId { get; set; }
    } 
    public class SearchPositionQueryHandler : IRequestHandler<SearchPositionQuery, List<SearchedPosition>>
    {
        private HCMContext _context;
        private readonly IMediator _mediator;
        public SearchPositionQueryHandler(HCMContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        } 
        public async Task<List<SearchedPosition>> Handle(SearchPositionQuery request, CancellationToken cancellationToken)
        {
            List<SearchedPosition> result = new List<SearchedPosition>();
            List<SearchedOrgPosition> OrgPositions = await _mediator.Send(new SearchOrgPositionQuery() { }).ConfigureAwait(false);
            if (request.Id != null)
            {
                result = await (from p in _context.Position
                                join w in _context.WorkArea on p.WorkingAreaId equals w.Id into pw
                                from rpw in pw.DefaultIfEmpty() 
                                join OP in OrgPositions on p.PositionTypeId equals OP.Id into OPs
                                from rops in OPs.DefaultIfEmpty() 
                                join ST in _context.SalaryType on p.SalaryTypeId equals ST.Id into STs
                                from str in STs.DefaultIfEmpty() 
                                join L in _context.Location on p.LocationId equals L.Id into Ls
                                from Lr in Ls.DefaultIfEmpty() 
                                join PT in _context.PlanType on p.PlanTypeId equals PT.Id into PTs
                                from PTr in PTs.DefaultIfEmpty() 
                                where p.Id == request.Id
                                select new SearchedPosition
                                { 
                                    Id = p.Id,
                                    ParentId = p.ParentId,
                                    NodeId = Convert.ToInt32(p.Id),
                                    ParentNodeId = Convert.ToInt32(p.ParentId),
                                    WorkingAreaId = p.WorkingAreaId,
                                    Code = p.Code,
                                    PositionTypeId = p.PositionTypeId,
                                    LocationId = p.LocationId,
                                    SalaryTypeId = p.SalaryTypeId,
                                    Sorter = p.Sorter,
                                    OrganoGramId = p.OrganoGramId,
                                    PlanTypeId = p.PlanTypeId,
                                    WorkAreaText = rpw.Title,
                                    RankText = rops.RankText,
                                    PositionTypeText = rops.PositionTypeText,
                                    OrgUnitText = rops.OrgUnitText,
                                    SalaryTypeText = str.Dari,
                                    LocationText = Lr.Dari,
                                    PlanTypeText = PTr.Name,
                                }).OrderBy(c=>c.Sorter).DefaultIfEmpty().ToListAsync(cancellationToken);

            }
            //if (request.ParentId != null)
            //{
            //    result = await (from p in _context.Position
            //                    join w in _context.WorkArea on p.WorkingAreaId equals w.Id into pw
            //                    from rpw in pw.DefaultIfEmpty()
            //                    join OP in OrgPositions on p.PositionTypeId equals OP.Id into OPs
            //                    from rops in OPs.DefaultIfEmpty()
            //                    join ST in _context.SalaryType on p.SalaryTypeId equals ST.Id into STs
            //                    from str in STs.DefaultIfEmpty()
            //                    join L in _context.Location on p.LocationId equals L.Id into Ls
            //                    from Lr in Ls.DefaultIfEmpty()
            //                    join PT in _context.PlanType on p.PlanTypeId equals PT.Id into PTs
            //                    from PTr in PTs.DefaultIfEmpty()
            //                    where p.ParentId == request.ParentId
            //                    select new SearchedPosition
            //                    {
            //                        Id = p.Id,
            //                        ParentId = p.ParentId,
            //                        NodeId = Convert.ToInt32(p.Id),
            //                        ParentNodeId = Convert.ToInt32(p.ParentId),
            //                        WorkingAreaId = p.WorkingAreaId,
            //                        Code = p.Code,
            //                        PositionTypeId = p.PositionTypeId,
            //                        LocationId = p.LocationId,
            //                        SalaryTypeId = p.SalaryTypeId,
            //                        Sorter = p.Sorter,
            //                        OrganoGramId = p.OrganoGramId,
            //                        PlanTypeId = p.PlanTypeId,
            //                        WorkAreaText = rpw.Title,
            //                        RankText = rops.RankText,
            //                        PositionTypeText = rops.PositionTypeText,
            //                        OrgUnitText = rops.OrgUnitText,
            //                        SalaryTypeText = str.Dari,
            //                        LocationText = Lr.Dari,
            //                        PlanTypeText = PTr.Name,
            //                    }).OrderBy(c => c.Sorter).DefaultIfEmpty().ToListAsync(cancellationToken);
            //}

            else if (request.OrganoGramId != null)
            {
                result = await (from position in _context.Position
                                join w in _context.WorkArea on position.WorkingAreaId equals w.Id into pw
                                from rpw in pw.DefaultIfEmpty()
                                join OP in OrgPositions on position.PositionTypeId equals OP.Id into OPs
                                from rops in OPs.DefaultIfEmpty()
                                join ST in _context.SalaryType on position.SalaryTypeId equals ST.Id into STs
                                from str in STs.DefaultIfEmpty()
                                join L in _context.Location on position.LocationId equals L.Id into Ls
                                from Lr in Ls.DefaultIfEmpty()
                                join PT in _context.PlanType on position.PlanTypeId equals PT.Id into PTs
                                from PTr in PTs.DefaultIfEmpty()
                                where position.OrganoGramId == request.OrganoGramId
                                select new SearchedPosition
                                {
                                    Id = position.Id,
                                    ParentId = position.ParentId,
                                    NodeId = Convert.ToInt32(position.Id),
                                    ParentNodeId = Convert.ToInt32(position.ParentId),
                                    WorkingAreaId = position.WorkingAreaId,
                                    Code = position.Code,
                                    PositionTypeId = position.PositionTypeId,
                                    LocationId = position.LocationId,
                                    SalaryTypeId = position.SalaryTypeId,
                                    Sorter = position.Sorter,
                                    OrganoGramId = position.OrganoGramId,
                                    PlanTypeId = position.PlanTypeId,
                                    WorkAreaText = rpw.Title,
                                    RankText = rops.RankText,
                                    PositionTypeText = rops.PositionTypeText,
                                    OrgUnitText = rops.OrgUnitText,
                                    SalaryTypeText = str.Dari,
                                    LocationText = Lr.Dari,
                                    PlanTypeText = PTr.Name,
                                }).OrderBy(c => c.Sorter).ToListAsync(cancellationToken);
            }
            return result;
        }
    }
}
