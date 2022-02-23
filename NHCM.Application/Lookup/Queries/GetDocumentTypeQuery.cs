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

namespace NHCM.Application.Lookup.Queries
{
    public class GetDocumentTypeQuery : IRequest<List<DocumentType>>
    {
        public int? ID { get; set; }
        public int? ScreenID { get; set; }
    }
    public class GetDocumentTypeQueryHandler : IRequestHandler<GetDocumentTypeQuery, List<DocumentType>>
    {
        private readonly HCMContext _context;
        public GetDocumentTypeQueryHandler(HCMContext context)
        {
            _context = context;
        }
        public async Task<List<DocumentType>> Handle(GetDocumentTypeQuery request, CancellationToken cancellationToken)
        {


            List<DocumentType> result = new List<DocumentType>();

            if(request.ID !=null)
            {
                result = await _context.DocumentTypes.Where(doc => doc.Id == request.ID).ToListAsync(cancellationToken);
            }
            else if(request.ScreenID !=null && request.ScreenID != default(int))
            {
                result = await _context.DocumentTypes.Where(doc => doc.ScreenId == request.ScreenID).ToListAsync(cancellationToken);
            }
            else
            {
                result = await _context.DocumentTypes.ToListAsync(cancellationToken);
            }


            return result;
            
        }
    }
}
