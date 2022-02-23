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
    public class DeletePersonEducationCommand : IRequest<string>
    {
        public int ID { get; set; }
    }
    public class DeletePersonEducationCommandHandler : IRequestHandler<DeletePersonEducationCommand, string>
    {
        private readonly HCMContext _context;
        public DeletePersonEducationCommandHandler()
        {
            _context = new HCMContext();
        }
        public async Task<string> Handle(DeletePersonEducationCommand request, CancellationToken cancellationToken)
        {


            using (_context)
            {
               

                Education toDeleteEducation = new Education();

                toDeleteEducation = await (from ed in _context.Education where ed.Id == request.ID select ed).FirstOrDefaultAsync();
                if (toDeleteEducation != null)
                {
                    _context.Education.Remove(toDeleteEducation);
                    await _context.SaveChangesAsync(cancellationToken);
                }

            }



            return string.Empty;
        }
    }
}
