using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using NHCM.Application.Recruitment.Models;
using System.Threading;
using System.Threading.Tasks;
using NHCM.Application.Recruitment.Queries;
using NHCM.Persistence.Infrastructure.Services;

namespace NHCM.Application.Recruitment.Commands
{
  public   class SavePersonPublicationCommand  : IRequest <List<SearchedPersonPublication>>
    {
        public decimal? Id { get; set; }
        public decimal PersonId { get; set; }
        public short PublicationTypeId { get; set; }
        public string Subject { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public string Isbn { get; set; }
        public int? NoofPages { get; set; }
    }


    public class SavePersonPublicationCommandHandler : IRequestHandler<SavePersonPublicationCommand, List<SearchedPersonPublication>>
    {
        private HCMContext _context;
        private IMediator _mediator;
        private readonly ICurrentUser _currentUser;
        public SavePersonPublicationCommandHandler(HCMContext context, IMediator mediator, ICurrentUser currentUser)
        {
            _context = context;
            _mediator = mediator;
            _currentUser = currentUser;
        }
        public async Task<List<SearchedPersonPublication>> Handle(SavePersonPublicationCommand request, CancellationToken cancellationToken)
        {
            List<SearchedPersonPublication> result = new List<SearchedPersonPublication>();

            if (request.Id == null || request.Id == default(int))
            {
                // Save
                int CurrentUserId = await _currentUser.GetUserId();

                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        using (_context)
                        {
                            Publication publication = new Publication()
                            {
                                PersonId = request.PersonId,
                                PublicationTypeId = request.PublicationTypeId,
                                Subject = request.Subject,
                                PublishDate = request.PublishDate,
                                ModifiedOn = request.ModifiedOn,
                                ModifiedBy = request.ModifiedBy,
                                ReferenceNo = request.ReferenceNo,
                                CreatedOn = request.CreatedOn,
                                CreatedBy = request.CreatedBy,
                                Isbn = request.Isbn,
                                NoofPages = request.NoofPages,
                            };
                            _context.Publication.Add(publication);
                            await _context.SaveChangesAsync(CurrentUserId, cancellationToken);

                            result = await _mediator.Send(new SearchPersonPublicationQuery() { Id = publication.Id });
                            transaction.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception();
                    }
                }

               
            }
            else
            {
                using (_context)
                {
                    Publication toUpdatePublication = await _context.Publication.Where(ms => ms.Id == request.Id).SingleOrDefaultAsync();
                    toUpdatePublication.PersonId = request.PersonId;
                    toUpdatePublication.PublicationTypeId = request.PublicationTypeId;
                    toUpdatePublication.Subject = request.Subject;
                    toUpdatePublication.PublishDate = request.PublishDate;
                    toUpdatePublication.ModifiedOn = request.ModifiedOn;
                    toUpdatePublication.ModifiedBy = request.ModifiedBy; 
                    toUpdatePublication.ReferenceNo = request.ReferenceNo;
                    toUpdatePublication.CreatedOn = request.CreatedOn;
                    toUpdatePublication.CreatedBy = request.CreatedBy;
                    toUpdatePublication.Isbn = request.Isbn;
                    toUpdatePublication.NoofPages = request.NoofPages;
                    await _context.SaveChangesAsync(cancellationToken);
                    result = await _mediator.Send(new SearchPersonPublicationQuery() { Id = toUpdatePublication.Id });
                }
            }
            return result;
        }
    }
}
