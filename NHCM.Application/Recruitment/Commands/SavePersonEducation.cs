using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using NHCM.Domain;
using NHCM.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using NHCM.Persistence.Extensions;
using NHCM.Persistence;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NHCM.Application.Recruitment.Queries;
using NHCM.Application.Recruitment.Models;
using NHCM.Application.Common;
using NHCM.Persistence.Infrastructure.Services;

namespace NHCM.Application.Recruitment.Commands
{
    public class SavePersonEducation : IRequest<List<SearchedPersonEducations>>
    {

        public decimal? Id { get; set; }
        public decimal PersonId { get; set; }
        public short EducationLevelId { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string OfficialDocumentNo { get; set; }
        public int? LocationId { get; set; }
        public string Institute { get; set; }
        public string Course { get; set; }
        public string Department { get; set; }
        public bool? Inservice { get; set; }
        public string Major { get; set; }
        public string Remarks { get; set; }
        public string MigratedLocation { get; set; }
        public string Faculty { get; set; }
    }


    public class SavePersonEducationHandler : IRequestHandler<SavePersonEducation, List<SearchedPersonEducations>>
    {

        private readonly HCMContext _context;
        private readonly IMediator _mediator;

        private readonly ICurrentUser _currentUser;

        public SavePersonEducationHandler(HCMContext context, IMediator mediator, ICurrentUser currentUser)
        {
            _context = context;
            _mediator = mediator;
            _currentUser = currentUser;

        }
        public async Task<List<SearchedPersonEducations>> Handle(SavePersonEducation request, CancellationToken cancellationToken)
        {

            List<SearchedPersonEducations> dbResult = new List<SearchedPersonEducations>();

            // Save
            if (request.Id == null || request.Id == default(decimal))
            {
                int CurrentUserId = await _currentUser.GetUserId();


                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        using (_context)
                        {
                            // Construct Education Object
                            Education education = new Education()
                            {
                                PersonId = request.PersonId,
                                EducationLevelId = request.EducationLevelId,
                                ModifiedOn = request.ModifiedOn,
                                ModifiedBy = request.ModifiedBy,
                                ReferenceNo = request.ReferenceNo,
                                CreatedOn = request.CreatedOn,
                                CreatedBy = request.CreatedBy,
                                StartDate = request.StartDate,
                                EndDate = request.EndDate,
                                OfficialDocumentNo = request.OfficialDocumentNo,
                                LocationId = request.LocationId,
                                Institute = request.Institute,
                                Course = request.Course,
                                Department = request.Department,
                                Inservice = request.Inservice,
                                Major = request.Major,
                                Remarks = request.Remarks,
                                MigratedLocation = request.MigratedLocation,
                                Faculty = request.Faculty
                            };

                            _context.Education.Add(education);
                            await _context.SaveChangesAsync(CurrentUserId, cancellationToken);
                            dbResult = await _mediator.Send(new SearchPersonEducationQuery() { Id = education.Id });

                            transaction.Commit();
                            return dbResult; 
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception();
                    }
                }

                
            }
            // Update
            else
            {
                using (_context)
                {
                    Education education = await (from e in _context.Education
                                                 where e.Id.Equals(request.Id.Value)
                                                 select e).FirstOrDefaultAsync();

                    education.PersonId = request.PersonId;
                    education.EducationLevelId = request.EducationLevelId;
                    education.ModifiedOn = DateTime.Now;
                    education.ModifiedBy = request.ModifiedBy;
                    education.ReferenceNo = request.ReferenceNo;
                    education.CreatedOn = request.CreatedOn;
                    education.CreatedBy = request.CreatedBy;
                    education.StartDate = request.StartDate;
                    education.EndDate = request.EndDate;
                    education.OfficialDocumentNo = request.OfficialDocumentNo;
                    education.LocationId = request.LocationId;
                    education.Institute = request.Institute;
                    education.Course = request.Course;
                    education.Department = request.Department;
                    education.Major = request.Major;
                    education.Remarks = request.Remarks;
                    education.Faculty = request.Faculty;
                    await _context.SaveChangesAsync();
                    dbResult = await _mediator.Send(new SearchPersonEducationQuery() { Id = education.Id });
                    return dbResult;
                }
            }
        }
    }
}
