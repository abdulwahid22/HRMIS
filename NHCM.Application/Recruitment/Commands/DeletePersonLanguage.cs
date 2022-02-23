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
    public class DeletePersonLanguageCommand : IRequest<string>
    {
        public int ID { get; set; }
    }
    public class DeletePersonLanguageCommandHandler : IRequestHandler<DeletePersonLanguageCommand, string>
    {
        private readonly HCMContext _context;
        public DeletePersonLanguageCommandHandler()
        {
            _context = new HCMContext();
        }
        public async Task<string> Handle(DeletePersonLanguageCommand request, CancellationToken cancellationToken)
        {


            using (_context)
            {
                PersonLanguage toDeletePersonLanguage = new PersonLanguage();

                toDeletePersonLanguage = await (from pl in _context.PersonLanguage where pl.Id == request.ID select pl).FirstOrDefaultAsync();
                if (toDeletePersonLanguage != null)
                {
                    _context.PersonLanguage.Remove(toDeletePersonLanguage);
                    await _context.SaveChangesAsync(cancellationToken);
                }

            }



            return string.Empty;
        }
    }
}
