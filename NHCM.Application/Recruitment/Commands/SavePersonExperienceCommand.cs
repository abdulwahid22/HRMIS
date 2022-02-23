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
   public class SavePersonExperienceCommand : IRequest<List<SearchedPersonExperience>>
    {
        public string Designation { get; set; }
        public decimal? Id { get; set; }
        public decimal PersonId { get; set; }
        public string Organization { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public short? RequestNo { get; set; }
        public int? LocationId { get; set; }
        public string DocumentNo { get; set; }
        public short? RankId { get; set; }
        public short? PromotionId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ContactInfo { get; set; }
        public int? JobstatusId { get; set; }
        public string JobDescription { get; set; }
        public bool? Approved { get; set; }
        public string Remarks { get; set; }
        public int? ExperienceTypeId { get; set; }
    }

    public class SavePersonExperienceCommandHandler : IRequestHandler<SavePersonExperienceCommand, List<SearchedPersonExperience>>
    {
        private readonly HCMContext _context;
        private readonly IMediator _mediator;
        private readonly ICurrentUser _currentUser;
        public SavePersonExperienceCommandHandler(HCMContext context, IMediator mediator, ICurrentUser currentUser)
        {
            _context = context;
            _mediator = mediator;
            _currentUser = currentUser;
        }
        public async Task<List<SearchedPersonExperience>> Handle(SavePersonExperienceCommand request, CancellationToken cancellationToken)
        {
            List<SearchedPersonExperience> result = new List<SearchedPersonExperience>();
            if( request.Id == null || request.Id == default(decimal))
            {
                // Save
                int CurrentUserId = await _currentUser.GetUserId();

                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        using (_context)
                        {
                            Experience personExperience = new Experience()
                            {
                                Designation = request.Designation,

                                PersonId = request.PersonId,
                                Organization = request.Organization,
                                ModifiedOn = request.ModifiedOn,
                                ModifiedBy = request.ModifiedBy,
                                ReferenceNo = request.ReferenceNo,
                                CreatedOn = request.CreatedOn,
                                CreatedBy = request.CreatedBy ?? 10, // UNTILL : application of idenity
                                RequestNo = request.RequestNo,
                                LocationId = request.LocationId,
                                DocumentNo = request.DocumentNo,
                                RankId = request.RankId,
                                PromotionId = request.PromotionId,
                                StartDate = request.StartDate,
                                EndDate = request.EndDate,
                                ContactInfo = request.ContactInfo,
                                JobstatusId = request.JobstatusId,
                                JobDescription = request.JobDescription,
                                Approved = request.Approved,
                                Remarks = request.Remarks,
                                ExperienceTypeId = request.ExperienceTypeId,
                            };

                            _context.Experience.Add(personExperience);
                            await _context.SaveChangesAsync(CurrentUserId, cancellationToken);

                            result = await _mediator.Send(new SearchPersonExperienceQuery() { Id = personExperience.Id });

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
                // Update

                Experience toUpdateExperience = await _context.Experience.Where(ex => ex.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
                toUpdateExperience.PersonId = request.PersonId;
                toUpdateExperience.Organization = request.Organization;
                toUpdateExperience.ModifiedOn = request.ModifiedOn;
                toUpdateExperience.ModifiedBy = request.ModifiedBy;
                toUpdateExperience.ReferenceNo = request.ReferenceNo;
                toUpdateExperience.CreatedOn = request.CreatedOn;
                toUpdateExperience.CreatedBy = request.CreatedBy ?? 10; // UNTILL : untill application of Identity
                toUpdateExperience.RequestNo = request.RequestNo;
                toUpdateExperience.LocationId = request.LocationId;
                toUpdateExperience.DocumentNo = request.DocumentNo;
                toUpdateExperience.RankId = request.RankId;
                toUpdateExperience.PromotionId = request.PromotionId;
                toUpdateExperience.StartDate = request.StartDate;
                toUpdateExperience.EndDate = request.EndDate;
                toUpdateExperience.ContactInfo = request.ContactInfo;
                toUpdateExperience.JobstatusId = request.JobstatusId;
                toUpdateExperience.JobDescription = request.JobDescription;
                toUpdateExperience.Approved = request.Approved;
                toUpdateExperience.Remarks = request.Remarks;
                toUpdateExperience.ExperienceTypeId = request.ExperienceTypeId;

                await _context.SaveChangesAsync(cancellationToken);

                result = await _mediator.Send(new SearchPersonExperienceQuery() { Id = toUpdateExperience.Id });
            }


            return result;
        }
    }
}
