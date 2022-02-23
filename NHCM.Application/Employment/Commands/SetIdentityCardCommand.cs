using MediatR;
using NHCM.Application.Employment.Models;
using NHCM.Application.Employment.Queries;
using NHCM.Application.Infrastructure.Exceptions;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using NHCM.Persistence.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NHCM.Application.Employment.Commands
{
   public  class SetIdentityCardCommand : IRequest<List<SearchedIdentityCardModel>>
    {

        public long Id { get; set; }
        public string CardCode { get; set; }
        public decimal? PersonId { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string PhotoPath { get; set; }
        public bool? CardPrinted { get; set; }
        public string FirstName { get; set; }
    }
    public class SetIdentityCardCommandHandler : IRequestHandler<SetIdentityCardCommand, List<SearchedIdentityCardModel>>
    {
        private readonly HCMContext _context;
        private readonly ICurrentUser _currentUser;
        private readonly IMediator _mediator;
        public SetIdentityCardCommandHandler(HCMContext context, ICurrentUser currentUser, IMediator mediator)
        {
            _context = context;
            _currentUser = currentUser;
            _mediator = mediator;
        }
        public async Task<List<SearchedIdentityCardModel>> Handle(SetIdentityCardCommand request, CancellationToken cancellationToken)
        {
            int CurrentUserId = await _currentUser.GetUserId();

            List<SearchedIdentityCardModel> listOfCards = new List<SearchedIdentityCardModel>();
            HCMContext _db = new HCMContext();
            var count = _db.IdentityCard.Count(x => x.PersonId.Equals(request.PersonId));
            if (count.Equals(0))
            {
                IdentityCard card = new IdentityCard()
                {
                    // Id= 1704,
                    CardCode = "A-NSIA",
                    PersonId = request.PersonId,
                    IssueDate = DateTime.Now,
                    ExpiryDate = request.ExpiryDate,
                    PhotoPath = request.PhotoPath,
                    StatusID = 0,
                    CreatedBy = CurrentUserId,

                };

                _context.IdentityCard.Add(card);
                await _context.SaveChangesAsync(cancellationToken);

                listOfCards = await _mediator.Send(new GetIdentityCardsQuery() { Id = card.Id });
            }

            else if(count>=1)
            {
                List<IdentityCard> results = (from p in _db.IdentityCard
                                              where p.PersonId.Equals(request.PersonId)
                                              select p).ToList();

                foreach (IdentityCard p in results)
                {
                   p.CardCode = "A-NSIA";
                    p.PersonId = request.PersonId;
                    p.IssueDate = DateTime.Now;
                    p.ExpiryDate = request.ExpiryDate;
                    p.PhotoPath = request.PhotoPath;
                    p.StatusID = 0;
                    p.CreatedBy = CurrentUserId;
                    listOfCards = await _mediator.Send(new GetIdentityCardsQuery() { Id = p.Id });
                }

                _db.SaveChanges();
                //throw new BusinessRulesException("کارمند قبلا درسیستم موجود است");
            }
            return listOfCards;
            
        }
    }
}
