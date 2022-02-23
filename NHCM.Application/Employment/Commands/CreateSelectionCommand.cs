using MediatR;
using System;
using System.Collections.Generic; 
using NHCM.Domain.Entities;
using System.Threading;
using System.Threading.Tasks; 
using NHCM.Persistence;
using System.Linq; 
using NHCM.Application.Employment.Models;
using NHCM.Application.Employment.Queries; 
using NHCM.Application.Infrastructure.Exceptions;

namespace NHCM.Application.Employment.Commands
{
    public class CreateSelectionCommand : IRequest<List<SearchedSelectionModel>>
    { 
        public decimal PositionId { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public decimal? Id { get; set; }
        public int EventTypeId { get; set; }
        public decimal PersonId { get; set; }
        public string Remarks { get; set; }
        public DateTime? VerdictDate { get; set; }
        public string VerdictRegNo { get; set; }
        public string FinalNo { get; set; } 
        public int OrganoGramID { get; set; }
        public short? QadamID { get; set; }
        public int DepartmentID { get; set; }
    }
     
    public class CreateSelectionCommandHandler : IRequestHandler<CreateSelectionCommand, List<SearchedSelectionModel>>
    { 
        private readonly HCMContext _context;
        private readonly IMediator _mediator;
        public CreateSelectionCommandHandler(HCMContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task<List<SearchedSelectionModel>> Handle(CreateSelectionCommand request, CancellationToken cancellationToken)
        {
            List<SearchedSelectionModel> result = new List<SearchedSelectionModel>();  
            if (request.Id == null || request.Id == default(decimal))
            {
                // Business Rule Check 1 : Check if person has already been selected for any position at the selected Tashkil 
                Selection SS = (from S in _context.Selection
                               join P in _context.Position on S.PositionId equals P.Id into SP
                               from spResult in SP.DefaultIfEmpty()
                               join org in _context.OrganoGram on spResult.OrganoGramId equals org.Id into spOrg
                               from spOrgResult in spOrg.DefaultIfEmpty()
                               where spOrgResult.Id == request.OrganoGramID && S.PersonId == request.PersonId
                              select S
                          ).SingleOrDefault();

                //if ((from S in _context.Selection 
                //     join P in _context.Position on S.PositionId equals P.Id into SP
                //     from spResult in SP.DefaultIfEmpty()
                //     join org in _context.OrganoGram on spResult.OrganoGramId equals org.Id into spOrg
                //     from spOrgResult in spOrg.DefaultIfEmpty()
                //     where spOrgResult.Id == request.OrganoGramID && S.PersonId == request.PersonId 
                //     select S).Any())
                if (SS != null)
                {
                    throw new BusinessRulesException("شخص انتخاب شده در تشکیل فعلی قبلا شاغل یکی از بست ها میباشد");
                } 
                else
                {
                    Selection selection = new Selection()
                    {

                        PositionId = request.PositionId,
                        PersonId = request.PersonId,
                        EffectiveDate = request.EffectiveDate,
                        VerdictDate = request.VerdictDate,
                        EventTypeId = request.EventTypeId,
                        VerdictRegNo = request.VerdictRegNo,
                        Remarks = request.Remarks,
                        FinalNo = request.FinalNo,
                        QadamID = request.QadamID,
                        DepartmentID = request.DepartmentID
                    };
                    _context.Selection.Add(selection);
                    await _context.SaveChangesAsync(cancellationToken);




                    result = await _mediator.Send(new SearchSelectionQuery() { PositionId = Convert.ToInt16(selection.PositionId) });
                    return result;
                }
            } 
            else
            {
                Selection d = (from p in _context.Selection
                               where p.Id == request.Id
                               select p).First();
                d.PositionId = request.PositionId;
                d.PersonId = request.PersonId;
                d.EventTypeId = request.EventTypeId;
                d.EffectiveDate = request.EffectiveDate;
                d.VerdictDate = request.VerdictDate;
                d.VerdictRegNo = request.VerdictRegNo;
                d.Remarks = request.Remarks;
                d.FinalNo = request.FinalNo;
                d.QadamID = request.QadamID;
                d.DepartmentID = request.DepartmentID;
                await _context.SaveChangesAsync(); 
                result = await _mediator.Send(new SearchSelectionQuery() { OrganoGramId = _context.Position.Where(a => a.Id == request.PositionId).SingleOrDefault().OrganoGramId });
                return result; 
            } 
        }
    }
}
