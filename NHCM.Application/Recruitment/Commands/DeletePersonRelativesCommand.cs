using MediatR;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NHCM.Application.Recruitment.Commands
{
    public class DeletePersonRelativesCommand : IRequest<string>
    {
        public int ID { get; set; }
    }
    public class DeletePersonRelativesCommandHandler : IRequestHandler<DeletePersonRelativesCommand, string>
    {
        private readonly HCMContext _context;
        public DeletePersonRelativesCommandHandler()
        {
            _context = new HCMContext();
        }
        public async Task<string> Handle(DeletePersonRelativesCommand request, CancellationToken cancellationToken)
        {


            using (_context)
            {
                Relative toDeleteRelative = new Relative();

                toDeleteRelative = (from r in _context.Relative where r.Id == request.ID select r).FirstOrDefault();
                if(toDeleteRelative !=null)
                {
                    _context.Relative.Remove(toDeleteRelative);
                   await _context.SaveChangesAsync(cancellationToken);
                }

            }



                return string.Empty;
        }
    }
}
