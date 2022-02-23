using MediatR;
using NHCM.Application.Recruitment.Models;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NHCM.Application.Recruitment.Queries
{
    public class SearchPersonTravelQuery:IRequest<List<SearchedPersonTravel>>
    {
        public decimal? Id { get; set; }
        public decimal? PersonId { get; set; }
        public int CountryId { get; set; }
        public string Place { get; set; }
        public DateTime? TravelDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Reason { get; set; }
        public string AccompanyWith { get; set; }
        public string ReferenceNo { get; set; }
       
        public string CountryText { get; set; }


        public String TravelDateText { get; set; }
        public String ReturnDateText { get; set; }
    }

    public class SearchPersonTravelHandler : IRequestHandler<SearchPersonTravelQuery, List<SearchedPersonTravel>>
    {
        private HCMContext _context;
        public SearchPersonTravelHandler(HCMContext context) { _context = context; }

        public async Task<List<SearchedPersonTravel>> Handle(SearchPersonTravelQuery request, CancellationToken cancellationToken)
        {
            List<SearchedPersonTravel> result = new List<SearchedPersonTravel>();
            if (request.Id != null)
            {
                using (_context)
                {
                    result = await (from pt in _context.Travel
                                    join l in _context.Location on pt.CountryId equals l.Id

                                    where pt.Id == request.Id
                                    select new SearchedPersonTravel
                                    {
                                        Id = pt.Id,
                                        PersonId = pt.PersonId,
                                        CountryId = pt.CountryId,
                                        Place = pt.Place,
                                        TravelDate = pt.TravelDate,
                                        ReturnDate = pt.ReturnDate,
                                        Reason = pt.Reason,
                                        AccompanyWith = pt.AccompanyWith,
                                        ReferenceNo = pt.ReferenceNo, 
                                        CountryText = l.Dari,

                                        TravelDateText = PersianLibrary.PersianDate.GetFormatedString(pt.TravelDate.Value),
                                        ReturnDateText = PersianLibrary.PersianDate.GetFormatedString(pt.ReturnDate.Value)


                                    }).OrderByDescending(t => t.TravelDate).ToListAsync(cancellationToken);
                }
            }

            else if (request.PersonId != null)
            {
                using (_context)
                {
                    result = await (from pt in _context.Travel
                                    join l in _context.Location on pt.CountryId equals l.Id

                                    where pt.PersonId == request.PersonId
                                    select new SearchedPersonTravel
                                    {
                                        Id = pt.Id,
                                        PersonId = pt.PersonId,
                                        CountryId = pt.CountryId,
                                        Place = pt.Place,
                                        TravelDate = pt.TravelDate,
                                        ReturnDate = pt.ReturnDate,
                                        Reason = pt.Reason,
                                        AccompanyWith = pt.AccompanyWith,
                                        ReferenceNo = pt.ReferenceNo, 
                                        CountryText = l.Dari,


                                        TravelDateText = PersianLibrary.PersianDate.GetFormatedString(pt.TravelDate.Value),
                                        ReturnDateText = PersianLibrary.PersianDate.GetFormatedString(pt.ReturnDate.Value)


                                    }).OrderByDescending(t => t.TravelDate).ToListAsync(cancellationToken);
                }
            }
            return result;

        }
    }
}
