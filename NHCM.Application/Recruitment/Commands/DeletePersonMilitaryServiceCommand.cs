using MediatR;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace NHCM.Application.Recruitment.Commands
{
   public  class DeletePersonMilitaryServiceCommand : IRequest<string>
    {
        public int? ID { get; set; }
    }

    public class DeletePersonMilitaryServiceCommandHandler : IRequestHandler<DeletePersonMilitaryServiceCommand, string>
    {
        private readonly HCMContext _context;
        public DeletePersonMilitaryServiceCommandHandler()
        {
            _context = new HCMContext();
        }
        public async Task<string> Handle(DeletePersonMilitaryServiceCommand request, CancellationToken cancellationToken)
        {
            if (request.ID != null)
            {
                using (_context)
                {


                    MilitaryService toDeleteMilitaryService = new MilitaryService();

                    toDeleteMilitaryService = await (from ex in _context.MilitaryService where ex.Id == request.ID select ex).FirstOrDefaultAsync();
                    if (toDeleteMilitaryService != null)
                    {
                        _context.MilitaryService.Remove(toDeleteMilitaryService);
                        await _context.SaveChangesAsync(cancellationToken);
                    }


                }



                return string.Empty;
            }

            return string.Empty;
        }
    }
}
