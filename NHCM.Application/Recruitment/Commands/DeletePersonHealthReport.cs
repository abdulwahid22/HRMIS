using MediatR;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NHCM.Application.Recruitment.Commands
{
     public class DeletePersonHealthReport : IRequest<string>
    {
        public int Id { get; set; }
    }

    public class DeletePersonHealthReportHandler : IRequestHandler <DeletePersonHealthReport, string>
    {
        private readonly HCMContext _context;
        public DeletePersonHealthReportHandler()
        {
            _context = new HCMContext();
        }

        public async Task<string> Handle(DeletePersonHealthReport request, CancellationToken cancellationToken)
        {
            using (_context)
            {
                HealthReport toDeletePersonHealthReport = new HealthReport();

                toDeletePersonHealthReport = await (from a in _context.HealthReport where a.Id == request.Id select a).FirstOrDefaultAsync();
                if (toDeletePersonHealthReport != null)
                {
                    _context.HealthReport.Remove(toDeletePersonHealthReport);
                    await _context.SaveChangesAsync(cancellationToken);
                }

            }

            return string.Empty;

        }
    }
}
