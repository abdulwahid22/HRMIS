using MediatR;
using NHCM.Application.Common;
using NHCM.Application.Recruitment.Models;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NHCM.Application.Recruitment.Queries
{
    public class SearchPersonLanguageQuery : IRequest<List<SearchedPersonLanguage>>
    {



        public decimal? Id { get; set; }
        public decimal? PersonId { get; set; }
        public short LanguageId { get; set; }
        public short? ReadingExpertise { get; set; }
        public short? UnderstandingExpertise { get; set; }
        public short? WritingExpertise { get; set; }
        public short? SpeakingExpertise { get; set; }


    }


    public class SearchPersonLanguageQueryHandler : IRequestHandler<SearchPersonLanguageQuery, List<SearchedPersonLanguage>>
    {

        private readonly HCMContext _context;
      

        public SearchPersonLanguageQueryHandler(HCMContext context/*, IMediator mediator*/)
        {
            _context = context;
           
        }

        public async Task<List<SearchedPersonLanguage>> Handle(SearchPersonLanguageQuery request, CancellationToken cancellationToken)
        {

            PersonCommon common = new PersonCommon(_context);
            List<SearchedPersonLanguage> result = new List<SearchedPersonLanguage>();
            result = await common.SearchPersonLanguages(request, cancellationToken);

            return result;

        }
    }
}
