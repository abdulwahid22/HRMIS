using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using Microsoft.EntityFrameworkCore;

namespace NHCM.Application.Lookup.Queries
{

    /// <summary>
    /// This request only includes the language id. If provided, the specific lanaugage will be returned as response. If not provided
    /// all the languages will be returned.
    /// </summary>
    public class GetLanguageQuery : IRequest<List<Language>>
    {
       
        public int? ID { get; set; }
    }

    public class GetLanguageQueryHandler : IRequestHandler<GetLanguageQuery, List<Language>>
    {
        private readonly HCMContext _dbContext;
        public GetLanguageQueryHandler(HCMContext context)
        {
            _dbContext = context;
        }


        public async Task<List<Language>> Handle(GetLanguageQuery request, CancellationToken cancellationToken)
        {
            List<Language> languages = new List<Language>();

            if (request.ID != null)
            {
                // Return specific language.
                languages = await _dbContext.Language.Where(l => l.Id == request.ID).ToListAsync(cancellationToken);
                return languages;
            }
            else
            {
                // Return all languages.
                languages = await _dbContext.Language.ToListAsync(cancellationToken);
                return languages;

            }


        }
    }
}
