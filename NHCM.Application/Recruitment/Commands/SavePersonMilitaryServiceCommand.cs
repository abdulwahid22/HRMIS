using MediatR;
using Microsoft.EntityFrameworkCore;
using NHCM.Application.Recruitment.Models;
using NHCM.Application.Recruitment.Queries;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using NHCM.Persistence.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NHCM.Application.Recruitment.Commands
{
    public class SavePersonMilitaryServiceCommand : IRequest<List<SearchedPersonMilitaryService>>
    {
        public int? Id { get; set; }
        public decimal PersonId { get; set; }
        public int MilitaryServiceTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string Remark { get; set; }
    }


    public class SavePersonMilitaryServiceCommandHandler : IRequestHandler<SavePersonMilitaryServiceCommand, List<SearchedPersonMilitaryService>>
    {
        private HCMContext _context;
        private IMediator _mediator;
        private readonly ICurrentUser _currentUser;
        public SavePersonMilitaryServiceCommandHandler(HCMContext context, IMediator mediator, ICurrentUser currentUser)
        {
            _context = context;
            _mediator = mediator;
            _currentUser = currentUser;
        }
        public async Task<List<SearchedPersonMilitaryService>> Handle(SavePersonMilitaryServiceCommand request, CancellationToken cancellationToken)
        {
            List<SearchedPersonMilitaryService> result = new List<SearchedPersonMilitaryService>();

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
                            MilitaryService militaryService = new MilitaryService()
                            {

                                PersonId = request.PersonId,
                                MilitaryServiceTypeId = request.MilitaryServiceTypeId,
                                StartDate = request.StartDate,
                                EndDate = request.EndDate,
                                CreatedBy = request.CreatedBy,
                                CreatedOn = request.CreatedOn,
                                ModifiedBy = request.ModifiedBy,
                                ModifiedOn = request.ModifiedOn,
                                Remark = request.Remark
                            };
                            _context.MilitaryService.Add(militaryService);
                            await _context.SaveChangesAsync(CurrentUserId, cancellationToken);

                            result = await _mediator.Send(new SearchPersonMilitaryServiceQuery() { Id = militaryService.Id });
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
                    MilitaryService toUpdateMilitaryService = await _context.MilitaryService.Where(ms => ms.Id == request.Id).SingleOrDefaultAsync();




                    toUpdateMilitaryService. PersonId = request.PersonId;
                     toUpdateMilitaryService.MilitaryServiceTypeId = request.MilitaryServiceTypeId;
                     toUpdateMilitaryService.StartDate = request.StartDate;
                      toUpdateMilitaryService.EndDate = request.EndDate;
                      toUpdateMilitaryService.CreatedBy = request.CreatedBy;
                      toUpdateMilitaryService.CreatedOn = request.CreatedOn;
                      toUpdateMilitaryService.ModifiedBy = request.ModifiedBy;
                      toUpdateMilitaryService.ModifiedOn = request.ModifiedOn;
                    toUpdateMilitaryService.Remark = request.Remark;

                    await _context.SaveChangesAsync(cancellationToken);

                    result = await _mediator.Send(new SearchPersonMilitaryServiceQuery() { Id = toUpdateMilitaryService.Id });

                }
                
            }


            return result;
        }
    }
}
