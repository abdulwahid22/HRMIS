using MediatR;
using Microsoft.EntityFrameworkCore;
using NHCM.Application.Employment.Models;
using NHCM.Application.Employment.Queries;
using NHCM.Application.Infrastructure.Exceptions;
using NHCM.Domain.Entities;
using NHCM.Domain.ViewsEntities;
using NHCM.Persistence;
using NHCM.Persistence.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NHCM.Application.Employment.Queries
{
 public  class GetZoneCardQuery: IRequest<List<ZoneCardDetails>>
    {
        public string HrCode { get; set; }
    }
    public class GetZoneCardQueryHandler : IRequestHandler<GetZoneCardQuery, List<ZoneCardDetails>>
    {
        private readonly HCMContext _context;
        private readonly ICurrentUser _currentUser;
        private readonly IMediator _mediator;
        public GetZoneCardQueryHandler(HCMContext context, ICurrentUser currentUser, IMediator mediator)
        {
            _context = context;
            _currentUser = currentUser;
            _mediator = mediator;
        }
        public async Task<List<ZoneCardDetails>> Handle(GetZoneCardQuery request, CancellationToken cancellationToken)
        {
            List<ZoneCardDetails> listOfCardData = new List<ZoneCardDetails>();
            List<ZoneCardDetails> Record = new List<ZoneCardDetails>();

            if (!string.IsNullOrEmpty(request.HrCode))
            {
                listOfCardData = await _context.ZoneCardDetails.FromSql("SELECT * FROM public.zonecarddetails").ToListAsync();
                Record = listOfCardData.Where(h => h.Hrcode == request.HrCode).ToList();
                if (Record.Any())
                {
                    return Record;
                }
                else
                {
                    throw new BusinessRulesException("لست خالی میباشد");
                }
            }
            else
            {
                throw new BusinessRulesException("کودکادری خالی بوده نمیتواند");
            }

        }
    }
}
