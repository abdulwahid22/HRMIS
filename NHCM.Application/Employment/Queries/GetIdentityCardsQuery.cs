using MediatR;
using Microsoft.EntityFrameworkCore;
using NHCM.Application.Employment.Models;
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
    public class GetIdentityCardsQuery: IRequest<List<SearchedIdentityCardModel>>
    {
        public long? Id { get; set; }
        public decimal? PersonId { get; set; }
        public string FirstName { get; set; }
        public string ExpiryDate { get; set; }
    }

    public class GetIdentityCardsQueryHandler : IRequestHandler<GetIdentityCardsQuery, List<SearchedIdentityCardModel>>
    {
        private readonly HCMContext _context;
        private readonly ICurrentUser _currentUser;
        public GetIdentityCardsQueryHandler(HCMContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }
        public async Task<List<SearchedIdentityCardModel>> Handle(GetIdentityCardsQuery request, CancellationToken cancellationToken)
        {

            List<SearchedIdentityCardModel> listOfCards = new List<SearchedIdentityCardModel>();

            if(request.PersonId != null)
            {
                listOfCards = await (from c in _context.IdentityCard
                                     join p in _context.Person on c.PersonId equals p.Id into pe
                                     from rpw in pe.DefaultIfEmpty()
                                     where c.PersonId == request.PersonId
                              select new SearchedIdentityCardModel
                              {
                                  Id = c.Id,
                                  PersonId = c.PersonId,
                                  CardCode = c.CardCode,
                                  ExpiryDate = string.Format("{0:yyyy-MM-dd}", c.ExpiryDate),
                                  IssueDate = string.Format("{0:dd/MM/yyyy}", c.IssueDate),
                                  FirstName = rpw.FirstName,
                                  FatherName =rpw.FatherName,                                 
                                  CardPrinted  = (c.StatusID == 1) ? "بلی" : "نخیر"

                              }).ToListAsync();

                return listOfCards;

            }
             else if (request.Id != null)
            {
                listOfCards = await (from c in _context.IdentityCard
                                     join p in _context.Person on c.PersonId equals p.Id into pe
                                     from rpw in pe.DefaultIfEmpty()
                                     where c.Id == request.Id
                                     select new SearchedIdentityCardModel
                                     {
                                         Id = c.Id,
                                         PersonId = c.PersonId,
                                         CardCode = c.CardCode,
                                         ExpiryDate = string.Format("{0:yyyy-MM-dd}", c.ExpiryDate),
                                         IssueDate = string.Format("{0:dd/MM/yyyy}", c.IssueDate),
                                         FirstName = rpw.FirstName,
                                         FatherName = rpw.FatherName,
                                         CardPrinted = (c.StatusID == 1) ? "بلی" : "نخیر"

                                     }).ToListAsync();

                return listOfCards;

            }
            else
            {
                listOfCards = await (from c in _context.IdentityCard
                                     join p in _context.Person on c.PersonId equals p.Id into pe
                                     from rpw in pe.DefaultIfEmpty()
                                     select new SearchedIdentityCardModel
                                     {
                                         Id = c.Id,
                                         CardCode = c.CardCode,
                                         PersonId = c.PersonId,
                                         ExpiryDate = string.Format("{0:yyyy-MM-dd}", c.ExpiryDate),
                                         IssueDate = string.Format("{0:dd/MM/yyyy}", c.IssueDate),
                                         FirstName = rpw.FirstName,
                                         FatherName = rpw.FatherName,
                                         CardPrinted = (c.StatusID == 1) ? "بلی" : "نخیر"

                                     }).ToListAsync();
                return listOfCards;
            }

           
        }
    }
}
