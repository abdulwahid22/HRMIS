using MediatR;
using Microsoft.EntityFrameworkCore;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NHCM.Application.Recruitment.Commands
{
    public class DeletePersonAssetCommand:IRequest<string>
    {
        public int Id { get; set; }
    }


    public class DeletePersonAssetCommandHandler : IRequestHandler<DeletePersonAssetCommand , string>
    {
        private readonly HCMContext _context;
        public DeletePersonAssetCommandHandler()
        {
            _context = new HCMContext();
        }

        public async Task<string> Handle(DeletePersonAssetCommand request, CancellationToken cancellationToken)
        {
            using (_context)
            {
                PersonAsset toDeletePersonAsset = new PersonAsset();

                toDeletePersonAsset = await (from a in _context.PersonAsset where a.Id == request.Id select a).FirstOrDefaultAsync();
                if (toDeletePersonAsset != null)
                {
                    _context.PersonAsset.Remove(toDeletePersonAsset);
                    await _context.SaveChangesAsync(cancellationToken);
                }

            }

            return string.Empty;
        }
    }
}
