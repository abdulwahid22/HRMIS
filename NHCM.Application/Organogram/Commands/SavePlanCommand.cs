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
using Microsoft.EntityFrameworkCore;
using NHCM.Application.Lookup.Queries;
using NHCM.Application.Infrastructure.Exceptions;
using NHCM.Application.Organogram.Queries;
using NHCM.Persistence.Infrastructure.Services;

namespace NHCM.Application.Organogram.Commands
{
    public class SavePlanCommand : IRequest<List<SearchedPlan>>
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public int Year { get; set; }
        public int IsPositionsCopied { get; set; }
        public int NumberOfPositions { get; set; }
        public int StatusId { get; set; }
        public int ModuleID { get; set; }
        public int ProcessID { get; set; }

    }
    public class SaveOrganogramCommandHandler : IRequestHandler<SavePlanCommand, List<SearchedPlan>>
    {
        private readonly HCMContext _context;
        private readonly IMediator _mediator;
        private readonly ICurrentUser _currentUser;
        public SaveOrganogramCommandHandler(HCMContext context, IMediator mediator, ICurrentUser currentUser)
        {
            _context = context;
            _mediator = mediator;
            _currentUser = currentUser;

        }

        public async Task<List<SearchedPlan>> Handle(SavePlanCommand request, CancellationToken cancellationToken)
        {
            List<SearchedPlan> result = new List<SearchedPlan>();

            if (request.Id == default(decimal))
            {
                int CurrentUserId = await _currentUser.GetUserId();

                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        List<OrganoGram> List = _context.OrganoGram.Where(o => o.OrganizationId == request.OrganizationId && o.Year == request.Year).ToList();
                        if (List.Any())
                        {
                            throw new BusinessRulesException("اداره در سال انتخاب شده تشکیل دارد");
                        }
                        else
                        {
                            OrganoGram organogram = new OrganoGram()
                            {
                                OrganizationId = request.OrganizationId,
                                IsPositionsCopied = request.IsPositionsCopied,
                                StatusId = request.StatusId,
                                Year = request.Year,
                                NumberOfPositions = request.NumberOfPositions
                            };
                            _context.OrganoGram.Add(organogram);
                            await _context.SaveChangesAsync(CurrentUserId,cancellationToken);

                            if (request.IsPositionsCopied == 1)
                            {
                                OrganoGram orglast = (from a in _context.OrganoGram where a.Year == (request.Year - 1) && a.OrganizationId == request.OrganizationId select a).SingleOrDefault();
                                List<Position> list = _context.Position.Where(c => c.OrganoGramId == orglast.Id && c.ParentId == null).ToList();

                                if (list.Any())
                                {
                                    await CopyPositionsAsync(list.FirstOrDefault(), organogram.Id, null);
                                }
                            }
                            else
                            {
                                List<Organization> org = await _mediator.Send(new GetOrganiztionQuery() { Id = organogram.OrganizationId });
                                List<SearchedOrgPosition> orgp = await _mediator.Send(new SearchOrgPositionQuery() { Id = org.FirstOrDefault().OrgUnitTypeId, Children = false });
                                List<WorkArea> walist = (from a in _context.WorkArea where a.Title.Trim().Equals(org.FirstOrDefault().Dari.Trim()) select a).ToList();
                                if (!walist.Any())
                                {
                                    WorkArea a = new WorkArea();
                                    a.Title = org.FirstOrDefault().Dari.Trim();
                                    await _context.SaveChangesAsync(CurrentUserId, cancellationToken);
                                    walist = (from b in _context.WorkArea where b.Title.Trim().Equals(org.FirstOrDefault().Dari.Trim()) select b).ToList();
                                }
                                List<SearchedPosition> positionresults = await _mediator.Send(new SavePositionCommand()
                                {
                                    WorkingAreaId = Convert.ToInt32(walist.FirstOrDefault().Id),
                                    PositionTypeId = orgp.FirstOrDefault().Id,
                                    LocationId = 1,
                                    SalaryTypeId = 1,
                                    OrganoGramId = organogram.Id,
                                    Code = org.FirstOrDefault().Id.ToString() + "0000" + 1.ToString(),
                                    PlanTypeId = 1
                                });
                            }
                            result = await _mediator.Send(new Queries.SearchPlanQuery() { Id = organogram.Id });
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception();
                    }

                }
               
            }
            else
            {
                OrganoGram toUpdateRecord = await (from org in _context.OrganoGram
                                                   where org.Id == request.Id
                                                   select org).SingleOrDefaultAsync();
                toUpdateRecord.OrganizationId = request.OrganizationId;
                toUpdateRecord.StatusId = request.StatusId;
                toUpdateRecord.Year = request.Year;
                toUpdateRecord.NumberOfPositions = request.NumberOfPositions;
                await _context.SaveChangesAsync(cancellationToken);
                result = await _mediator.Send(new Queries.SearchPlanQuery()
                {

                    Id = toUpdateRecord.Id
                });
            }
            return result;
        } 
        public async Task<List<SearchedPosition>> AddPositionAsync(Position p, int PlanID, int? ParentID)
        {
            List<SearchedPosition> positionresults = await _mediator.Send(new SavePositionCommand()
            {
                WorkingAreaId = p.WorkingAreaId,
                ParentId = ParentID,
                PositionTypeId = p.PositionTypeId,
                LocationId = p.LocationId,
                SalaryTypeId = p.SalaryTypeId,
                OrganoGramId = PlanID,
                Code = p.Code,
                PlanTypeId = p.PlanTypeId
            });
            return positionresults;
        }
        public async Task CopyPositionsAsync(Position Position, int PlanID, int? ParentID)
        {
            List<SearchedPosition> result = await AddPositionAsync(Position, PlanID, ParentID);
            List<Position> list = (from a in _context.Position where a.ParentId == Position.Id select a).ToList();
            if (list.Any())
            {
                foreach (Position p in list)
                {
                    await CopyPositionsAsync(p, PlanID, Convert.ToInt32(result.SingleOrDefault().Id));
                }
            }

        }
    }
}
