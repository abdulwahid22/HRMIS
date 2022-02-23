using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using NHCM.Application.Recruitment.Models;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using NHCM.Persistence;
using NHCM.Application.Common;
using NHCM.Application.Employment.Models;
using NHCM.Application.Organogram.Models;
using NHCM.Application.Organogram.Queries;
using NHCM.Domain.Entities;

namespace NHCM.Application.Employment.Queries
{
    public class SearchSelectionQuery : IRequest<List<SearchedSelectionModel>>
    {
        public int? OrganoGramId { get; set; }
        public int? Id { get; set; }
        public int? PositionId { get; set; }
        public decimal? PersonID { get; set; }
    }


    public class SearchSelectionQueryHandler : IRequestHandler<SearchSelectionQuery, List<SearchedSelectionModel>>
    {
        private readonly HCMContext _context;
        private readonly IMediator _mediator;

        public SearchSelectionQueryHandler(HCMContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<List<SearchedSelectionModel>> Handle(SearchSelectionQuery request, CancellationToken cancellationToken)
        {
            List<SearchedSelectionModel> result = new List<SearchedSelectionModel>();
            List<SearchedPosition> Positions = new List<SearchedPosition>();  
            if (request.PositionId != null)
            {
                Positions = await _mediator.Send(new SearchPositionQuery() { Id = request.PositionId });
            }             
            else
            {
                Positions = await _mediator.Send(new SearchPositionQuery() { OrganoGramId = request.OrganoGramId });
            }        
            if (request.Id != null)
            {
                SearchedSelectionModel Record = await (from Selection in _context.Selection
                                                       join Person in _context.Person on Selection.PersonId equals Person.Id into Persons
                                                       from PersonResult in Persons.DefaultIfEmpty()
                                                       join P in Positions on Selection.PositionId equals P.Id into PositionsList
                                                       from PositionResult in PositionsList.DefaultIfEmpty()
                                                       where Selection.PositionId == PositionResult.Id
                                                       select new SearchedSelectionModel
                                                       {
                                                           PersonName = PersonResult.FirstName + " " + PersonResult.LastName,
                                                           SelectionId = Selection.Id,
                                                           Id = Selection.Id,
                                                           EffectiveDate = Selection.EffectiveDate,
                                                           EventTypeId = Selection.EventTypeId,
                                                           Remarks = Selection.Remarks,
                                                           FinalNo = Selection.FinalNo,
                                                           QadamID = Selection.QadamID,
                                                           VerdictDate = Selection.VerdictDate,
                                                           VerdictRegNo = Selection.VerdictRegNo,
                                                           PersonId = Selection.PersonId,
                                                           NodeId = Convert.ToInt32(PositionResult.Id),
                                                           PositionId = Convert.ToInt32(PositionResult.Id),
                                                           ParentNodeId = Convert.ToInt32(PositionResult.ParentId),
                                                           OrganogramId = Convert.ToInt32(PositionResult.OrganoGramId),
                                                           WorkAreaText = PositionResult.WorkAreaText,
                                                           PositionTypeText = PositionResult.PositionTypeText,
                                                           RankText = PositionResult.RankText,
                                                           OrgUnitText = PositionResult.OrgUnitText,
                                                           Sorter = PositionResult.Sorter,
                                                           Code = PositionResult.Code,
                                                           LocationText = PositionResult.LocationText,
                                                           DepartmentID = Selection.DepartmentID,
                                                           Title = PositionResult.PositionTypeText + " " + PositionResult.WorkAreaText
                                                       }).SingleOrDefaultAsync(cancellationToken);
                result.Add(Record);
            }
            else
            {
                foreach (SearchedPosition P in Positions)
                {
                    SearchedSelectionModel Record = new SearchedSelectionModel();
                    if (_context.Selection.Any(c => c.PositionId == P.Id))
                    {
                        Record = await (from Selection in _context.Selection
                                        join Person in _context.Person on Selection.PersonId equals Person.Id into Persons
                                        from PersonResult in Persons.DefaultIfEmpty()
                                        where Selection.PositionId == P.Id 
                                        select new SearchedSelectionModel
                                        {
                                            PersonName = PersonResult.FirstName + " " + PersonResult.LastName,
                                            SelectionId = Selection.Id,
                                            Id = Selection.Id,
                                            EffectiveDate = Selection.EffectiveDate,
                                            EventTypeId = Selection.EventTypeId,
                                            Remarks = Selection.Remarks,
                                            FinalNo = Selection.FinalNo, 
                                            VerdictDate = Selection.VerdictDate,
                                            VerdictRegNo = Selection.VerdictRegNo,
                                            PersonId = Selection.PersonId,
                                            DepartmentID = Selection.DepartmentID
                                        }).SingleOrDefaultAsync(cancellationToken);
                    }
                    Record.NodeId = Convert.ToInt32(P.Id);
                    Record.PositionId = Convert.ToInt32(P.Id);
                    Record.ParentNodeId = Convert.ToInt32(P.ParentId);
                    Record.OrganogramId = Convert.ToInt32(P.OrganoGramId);
                    Record.WorkAreaText = P.WorkAreaText;
                    Record.PositionTypeText = P.PositionTypeText;
                    Record.RankText = P.RankText;
                   
                    Record.OrgUnitText = P.OrgUnitText;
                    Record.Sorter = P.Sorter;
                    Record.Code = P.Code;
                    Record.RankText = P.RankText;
                    Record.LocationText = P.LocationText;
                    Record.Title = P.PositionTypeText + " " + P.WorkAreaText;
                    result.Add(Record);
                }
            }
            return result;
        }
    }
}
