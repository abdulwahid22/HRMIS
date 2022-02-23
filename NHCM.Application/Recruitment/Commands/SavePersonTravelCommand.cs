using MediatR;
using NHCM.Application.Recruitment.Models;
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
    public class SavePersonTravelCommand : IRequest<List<SearchedPersonTravel>>
    {
        public decimal? Id { get; set; }
        public decimal PersonId { get; set; }
        public int CountryId { get; set; }
        public string Place { get; set; }
        public DateTime? TravelDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Reason { get; set; }
        public string AccompanyWith { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }

        
    }
    
    public class SavePersonTravelCommandHandler : IRequestHandler<SavePersonTravelCommand, List<SearchedPersonTravel>>
    {
        private readonly HCMContext _context;
        private readonly IMediator _mediator;

        public SavePersonTravelCommandHandler(HCMContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;

        }
        
        public async Task<List<SearchedPersonTravel>> Handle(SavePersonTravelCommand request, CancellationToken cancellationToken)
        {

            
            List<SearchedPersonTravel> result = new List<SearchedPersonTravel>();
            

            if (request.Id == null || request.Id == default(decimal))
            {
                using (_context)
                {
                    Travel PersonTravel = new Travel()
                    {

                        PersonId = request.PersonId,
                        CountryId = request.CountryId,
                        Place = request.Place,
                        TravelDate = request.TravelDate,
                        ReturnDate = request.ReturnDate,
                        Reason = request.Reason,
                        AccompanyWith = request.AccompanyWith,
                        ReferenceNo = request.ReferenceNo,
                        CreatedOn = request.CreatedOn,
                        CreatedBy = request.CreatedBy
                        
                    };
                    _context.Travel.Add(PersonTravel);
                    await _context.SaveChangesAsync(cancellationToken);


                    result = await _mediator.Send(new Queries.SearchPersonTravelQuery() { Id = PersonTravel.Id });

                   
                }
            }
            else
            {
                using (_context)
                {
                    Travel toUpdateRecord = await(from pt in _context.Travel
                                                       where pt.Id == request.Id
                                                       select pt).SingleOrDefaultAsync();

                                       toUpdateRecord.PersonId = request.PersonId;
                                       toUpdateRecord.CountryId = request.CountryId;
                                       toUpdateRecord.Place = request.Place;
                                       toUpdateRecord.TravelDate = request.TravelDate;
                                       toUpdateRecord.ReturnDate = request.ReturnDate;
                                       toUpdateRecord.Reason = request.Reason;
                                       toUpdateRecord.AccompanyWith = request.AccompanyWith;
                                       toUpdateRecord.ReferenceNo = request.ReferenceNo;
                                       toUpdateRecord.CreatedOn = request.CreatedOn;
                                       toUpdateRecord.CreatedBy = request.CreatedBy;


                    await _context.SaveChangesAsync(cancellationToken);
                    result = await _mediator.Send(new Queries.SearchPersonTravelQuery() { Id = toUpdateRecord.Id });
                }
            }

            return result;
        }
    }
}
