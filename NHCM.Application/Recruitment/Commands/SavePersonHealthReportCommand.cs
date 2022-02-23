using MediatR;
using NHCM.Application.Recruitment.Models;
using NHCM.Application.Recruitment.Queries;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NHCM.Persistence.Infrastructure.Services;

namespace NHCM.Application.Recruitment.Commands
{
    public class SavePersonHealthReportCommand:IRequest<List<SearchedPersonHealthReport>>
    {
        public decimal? Id { get; set; }
        public decimal PersonId { get; set; }
        public DateTime ReportDate { get; set; }
        public int StatusId { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public bool? Approved { get; set; }
        public string Remarks { get; set; }
    }

    public class SavePersonHealthReportCommandHandler:IRequestHandler<SavePersonHealthReportCommand, List<SearchedPersonHealthReport>>
    {
        private readonly HCMContext _context;
        private readonly IMediator _mediator;
        private readonly ICurrentUser _currentUser;
        public SavePersonHealthReportCommandHandler(HCMContext context, IMediator mediator, ICurrentUser currentUser)
        {
            _context = context;
            _mediator = mediator;
            _currentUser = currentUser;
        }

        public async Task<List<SearchedPersonHealthReport>> Handle(SavePersonHealthReportCommand request, CancellationToken cancellationToken)
        {

            List<SearchedPersonHealthReport> result = new List<SearchedPersonHealthReport>();


            if (request.Id == null || request.Id == default(decimal))
            {

                int CurrentUserId = await _currentUser.GetUserId();

                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        using (_context)
                        {
                            HealthReport healthReport = new HealthReport()
                            {

                                PersonId = request.PersonId,
                                ReportDate = request.ReportDate,
                                StatusId = request.StatusId,
                                ModifiedOn = request.ModifiedOn,
                                ModifiedBy = request.ModifiedBy,
                                ReferenceNo = request.ReferenceNo,
                                CreatedOn = request.CreatedOn,
                                CreatedBy = request.CreatedBy,
                                Approved = request.Approved,
                                Remarks = request.Remarks
                            };

                            _context.HealthReport.Add(healthReport);
                            await _context.SaveChangesAsync(CurrentUserId, cancellationToken);

                            result = await _mediator.Send(new Queries.SearchPersonHealthReportQuery() { Id = healthReport.Id });
                            transaction.Commit();
                        }
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
                using (_context)
                {
                    HealthReport toUpdateRecord = await (from ph in _context.HealthReport 
                                                         where ph.Id == request.Id
                                                         select ph ).SingleOrDefaultAsync();

                     
                       toUpdateRecord.PersonId = request.PersonId;
                       toUpdateRecord.ReportDate = request.ReportDate;
                       toUpdateRecord.StatusId = request.StatusId;
                       toUpdateRecord.ModifiedOn = request.ModifiedOn;
                       toUpdateRecord.ModifiedBy = request.ModifiedBy;
                       toUpdateRecord.ReferenceNo = request.ReferenceNo;
                       toUpdateRecord.CreatedOn = request.CreatedOn;
                       toUpdateRecord.CreatedBy = request.CreatedBy;
                       toUpdateRecord.Approved = request.Approved;
                       toUpdateRecord.Remarks = request.Remarks;

                    await _context.SaveChangesAsync(cancellationToken);

                    result = await _mediator.Send(new Queries.SearchPersonHealthReportQuery() { Id = toUpdateRecord.Id });
                }
            }
            return result;

        }
    }
}
