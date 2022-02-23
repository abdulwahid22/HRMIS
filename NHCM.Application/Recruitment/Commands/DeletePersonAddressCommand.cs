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
   


    public class DeletePersonAddressCommand : IRequest<string>
    {
        public int ID { get; set; }
    }
    public class DeletePersonAddressCommandHandler : IRequestHandler<DeletePersonAddressCommand, string>
    {
        private readonly HCMContext _context;
        public DeletePersonAddressCommandHandler()
        {
            _context = new HCMContext();
        }
        public async Task<string> Handle(DeletePersonAddressCommand request, CancellationToken cancellationToken)
        {


            using (_context)
            {


                Address toDeleteAddress = new Address();

                toDeleteAddress = await (from ed in _context.Address where ed.Id == request.ID select ed).FirstOrDefaultAsync();
                if (toDeleteAddress != null)
                {
                    _context.Address.Remove(toDeleteAddress);
                    await _context.SaveChangesAsync(cancellationToken);
                }

            }



            return string.Empty;
        }
    }
}
