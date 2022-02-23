using MediatR;
using NHCM.Application.Recruitment.Models;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace NHCM.Application.Recruitment.Queries
{
    public class SearchPersonEducationQuery : IRequest<List<SearchedPersonEducations>>
    {

        // One of these two fields should have a value in runtime else the search will not return any result set.
        public decimal? Id { get; set; }
        public decimal? PersonId { get; set; }
        public short EducationLevelId { get; set; }
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
        public short EducationLevelText { get; set; }
    }


    public class SearchPersonEducationQueryHandler : IRequestHandler<SearchPersonEducationQuery, List<SearchedPersonEducations>>
    {
        private HCMContext _context;
        public SearchPersonEducationQueryHandler(HCMContext context)
        {
            _context = context;
        }
        public async Task<List<SearchedPersonEducations>> Handle(SearchPersonEducationQuery request, CancellationToken cancellationToken)
        {
            List<SearchedPersonEducations> result = new List<SearchedPersonEducations>();
            if (request.Id != null)
            {
                result = await (from e in _context.Education
                                join l in _context.Location on e.LocationId equals l.Id into el
                                from resultEL in el.DefaultIfEmpty()
                                join lv in _context.EducationLevel on e.EducationLevelId equals lv.Id into elv
                                from resultElv in elv.DefaultIfEmpty()
                                where e.Id == request.Id
                                select new SearchedPersonEducations
                                {
                                    Id = e.Id,
                                    PersonId = e.PersonId,
                                    EducationLevelId = e.EducationLevelId,
                                    EducationLevelText = resultElv.Name,
                                    StartDate = e.StartDate,

                                    EndDate = e.EndDate,
                                    StartDateText = PersianLibrary.PersianDate.GetFormatedString(e.StartDate.Value),
                                    EndDateText = PersianLibrary.PersianDate.GetFormatedString(e.EndDate.Value),
                                    LocationId = e.LocationId,
                                    LocationText = resultEL.Dari,
                                    Institute = e.Institute,
                                    Faculty = e.Faculty,
                                    Department = e.Department,
                                    Course = e.Course,
                                    Major = e.Major,
                                    Remarks = e.Remarks,
                                    OfficialDocumentNo = e.OfficialDocumentNo,

                                }).OrderByDescending(c => c.EndDate).ToListAsync(cancellationToken);
            }
            else if (request.PersonId != null)
            {
                result = await (from e in _context.Education
                                join l in _context.Location on e.LocationId equals l.Id into el
                                from resultEL in el.DefaultIfEmpty()
                                join lv in _context.EducationLevel on e.EducationLevelId equals lv.Id into elv
                                from resultElv in elv.DefaultIfEmpty()
                                where e.PersonId == request.PersonId
                                select new SearchedPersonEducations
                                {
                                    Id = e.Id,
                                    PersonId = e.PersonId,
                                    EducationLevelId = e.EducationLevelId,
                                    EducationLevelText = resultElv.Name,
                                    StartDate = e.StartDate,

                                    EndDate = e.EndDate,
                                    StartDateText = PersianLibrary.PersianDate.GetFormatedString(e.StartDate.Value),
                                    EndDateText = PersianLibrary.PersianDate.GetFormatedString(e.EndDate.Value),
                                    LocationId = e.LocationId,
                                    LocationText = resultEL.Dari,
                                    Institute = e.Institute,
                                    Faculty = e.Faculty,
                                    Department = e.Department,
                                    Course = e.Course,
                                    Major = e.Major,
                                    Remarks = e.Remarks,
                                    OfficialDocumentNo = e.OfficialDocumentNo,

                                }).OrderByDescending(c=>c.EndDate).ToListAsync(cancellationToken);
            }
            return result;
        }
    }
}
