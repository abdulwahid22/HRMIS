using MediatR;
using NHCM.Application.Recruitment.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NHCM.Persistence;
using Microsoft.EntityFrameworkCore;

namespace NHCM.Application.Recruitment.Queries
{
    public class SearchPersonHealthReportQuery:IRequest<List<SearchedPersonHealthReport>>
    {
        public decimal? Id { get; set; }
        public decimal? PersonId { get; set; }
        public DateTime ReportDate { get; set; }
        public int? StatusId { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public bool? Approved { get; set; }
        public string Remarks { get; set; }


        public String ReportDateText { get; set; } 

    }

    public class SearchPersonHealthReportQueryHandler : IRequestHandler<SearchPersonHealthReportQuery, List<SearchedPersonHealthReport>>
    {
        private HCMContext _context;
        public SearchPersonHealthReportQueryHandler(HCMContext context) { _context = context; }
        public async Task<List<SearchedPersonHealthReport>> Handle(SearchPersonHealthReportQuery request, CancellationToken cancellationToken)
        {

            List<SearchedPersonHealthReport> result = new List<SearchedPersonHealthReport>();
            if (request.Id != null)
            {
                using (_context)
                {
                    result = await (from phr in _context.HealthReport

                                    join status in _context.Status on phr.StatusId equals status.Id into hs
                                    from resulths in hs.DefaultIfEmpty()

                                    where phr.Id == request.Id
                                    select new SearchedPersonHealthReport
                                    {
                                        Id = phr.Id,
                                        PersonId = phr.PersonId,
                                        ReportDate = phr.ReportDate,
                                        ReportDateText = PersianLibrary.PersianDate.GetFormatedString(phr.ReportDate), 

                                        StatusId = phr.StatusId, 
                                        ReferenceNo = phr.ReferenceNo, 
                                        Approved = phr.Approved,
                                        Remarks = phr.Remarks,

                                        StatusText = resulths.Dari
                                       

                                    }).OrderBy(h => h.ReportDate).ToListAsync(cancellationToken);
                }
            }

            else if (request.PersonId != null)
            {

                using (_context)
                {
                    result = await (from phr in _context.HealthReport

                                    join status in _context.Status on phr.StatusId equals status.Id into hs
                                    from resulths in hs.DefaultIfEmpty()

                                    where phr.PersonId == request.PersonId
                                    select new SearchedPersonHealthReport
                                    {
                                        Id = phr.Id,
                                        PersonId = phr.PersonId,
                                        ReportDate = phr.ReportDate,

                                        ReportDateText = PersianLibrary.PersianDate.GetFormatedString(phr.ReportDate),
                                        StatusId = phr.StatusId, 
                                        ReferenceNo = phr.ReferenceNo, 
                                        Approved = phr.Approved,
                                        Remarks = phr.Remarks,

                                        StatusText = resulths.Dari


                                    }).OrderBy(h => h.ReportDate).ToListAsync(cancellationToken);
                }
            }
            return result;
        }
    }

}
 
 