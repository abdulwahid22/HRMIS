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
    public class GetCardDataQuery : IRequest<List<carddetails>>
    {
        public string HrCode { get; set; }

    }

    public class GetCardDataQueryHandler : IRequestHandler<GetCardDataQuery,List<carddetails>>
    {
        private readonly HCMContext _context;
        private readonly ICurrentUser _currentUser;
        private readonly IMediator _mediator;
        public GetCardDataQueryHandler(HCMContext context, ICurrentUser currentUser, IMediator mediator)
        {
            _context = context;
            _currentUser = currentUser;
            _mediator = mediator;
        }
        public async Task<List<carddetails>> Handle(GetCardDataQuery request, CancellationToken cancellationToken)
        {
            List<carddetails> listOfCardData = new List<carddetails>();
            List<carddetails> Record = new List<carddetails>();

            if (!string.IsNullOrEmpty(request.HrCode))
            {
                listOfCardData = await _context.Carddetails.FromSql("SELECT * FROM public.carddetails").ToListAsync();
                Record = listOfCardData.Where(h => h.hrcode == request.HrCode).ToList();
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
