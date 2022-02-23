using MediatR;
using NHCM.Application.Reports.Models;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NHCM.Domain.ReportEntities;

namespace NHCM.Application.Reports.Queries
{
    public class SearchStatisticalReportQuery : IRequest<List<StatisticalReport>>
    { 
        public string gender_type { get; set; }
        public string postion_type { get; set; }
        public int Coun { get; set; }
    }

    public class SearchStatisticalReportQueryHandler : IRequestHandler<SearchStatisticalReportQuery, List<StatisticalReport>>
    {
        private HCMContext _context;
        public SearchStatisticalReportQueryHandler(HCMContext context)
        {
            _context = context;
        }

        public async Task<List<StatisticalReport>> Handle(SearchStatisticalReportQuery request, CancellationToken cancellationToken)
        { 
            List<StatisticalReport> result = new List<StatisticalReport>();
            result = await _context.StatisticalReports.FromSql("select * from rpt.statisticalreport()").ToListAsync();
            return result;
        }
    }
}
