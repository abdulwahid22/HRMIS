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
    public class DeletePersonTravelCommand : IRequest<string>
    {
        public int ID { get; set; }
    }

    public class DeletePersonTravelCommandHandler : IRequestHandler<DeletePersonTravelCommand, string>
    {
        private readonly HCMContext _context;
        public DeletePersonTravelCommandHandler()
        {
            _context = new HCMContext();
        }
        public async Task<string> Handle(DeletePersonTravelCommand request, CancellationToken cancellationToken)
        {
            using (_context)
            {
                Travel toDeleteRelative = new Travel();

                toDeleteRelative = await (from t in _context.Travel where t.Id == request.ID select t).FirstOrDefaultAsync();
                if (toDeleteRelative != null)
                {
                    _context.Travel.Remove(toDeleteRelative);
                    await _context.SaveChangesAsync(cancellationToken);
                }

            }
            
            return string.Empty;
        }
    }

}
