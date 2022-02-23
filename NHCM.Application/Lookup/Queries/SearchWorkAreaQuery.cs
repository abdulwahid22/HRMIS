using MediatR;
using NHCM.Application.Lookup.Models;
using NHCM.Persistence;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NHCM.Application.Lookup.Queries
{
    public class SearchWorkAreaQuery : IRequest<List<SearchedWorkAreaModel>>
    {
        public decimal? Id { get; set; }
        public string Title { get; set; }
        public string TitleEng { get; set; }
    }

    public class SearchWordAreaQueryHandler : IRequestHandler<SearchWorkAreaQuery, List<SearchedWorkAreaModel>>
    { 
        private HCMContext _context;
        public SearchWordAreaQueryHandler(HCMContext context)
        {
            _context = context;
        }

        public async Task<List<SearchedWorkAreaModel>> Handle(SearchWorkAreaQuery request, CancellationToken cancellationToken)
        {
            List<SearchedWorkAreaModel> result = new List<SearchedWorkAreaModel>();

            if (request.Id != null)
            {
                result = await(from worka in _context.WorkArea 
                               where worka.Id == request.Id

                               select new SearchedWorkAreaModel
                               {
                                    Id = worka.Id,
                                    Title = worka.Title,
                                    TitleEng  = worka.TitleEng

                                }).ToListAsync(cancellationToken);
            }

            else if (request.Title != default(string) && request.Title !="")
            {
                result = await (from worka in _context.WorkArea
                                where worka.Title == request.Title
                                select new SearchedWorkAreaModel
                                {
                                    Id = worka.Id,
                                    Title = worka.Title,
                                    TitleEng = worka.TitleEng

                                }).ToListAsync(cancellationToken);
            }
            else
            {
                result = await (from worka in _context.WorkArea 

                                select new SearchedWorkAreaModel
                                {
                                    Id = worka.Id,
                                    Title = worka.Title,
                                    TitleEng = worka.TitleEng

                                }).ToListAsync(cancellationToken);
            }
            return result;
        }
    }
}
