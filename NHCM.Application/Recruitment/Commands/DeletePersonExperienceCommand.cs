using MediatR;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NHCM.Application.Recruitment.Commands
{
    public class DeletePersonExperienceCommand : IRequest<string>
    {
        public int? ID { get; set; }
    }

    public class DeletePersonExperienceCommandHandler : IRequestHandler<DeletePersonExperienceCommand, string>
    {

        private readonly HCMContext _context;
        public DeletePersonExperienceCommandHandler()
        {
            _context = new HCMContext();
        }
        public async Task<string> Handle(DeletePersonExperienceCommand request, CancellationToken cancellationToken)
        {
            if(request.ID != null)
            {
                using (_context)
                {


                    Experience toDeleteExperience = new Experience();

                    toDeleteExperience = await (from ex in _context.Experience where ex.Id == request.ID select ex).FirstOrDefaultAsync();
                    if (toDeleteExperience != null)
                    {
                        _context.Experience.Remove(toDeleteExperience);
                        await _context.SaveChangesAsync(cancellationToken);
                    }

                    
                }



                return string.Empty;
            }

            return string.Empty;

        }
    }
}
