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
  public   class SearchPersonExperienceQuery : IRequest<List<SearchedPersonExperience>>
    {
         
        public decimal? Id { get; set; }
        public decimal? PersonId { get; set; }

        public string Organization { get; set; }
        public string Designation { get; set; }
        public short? RequestNo { get; set; }
        public int? LocationId { get; set; }
        public string DocumentNo { get; set; }
        public short RankId { get; set; }
        public short PromotionId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ContactInfo { get; set; }
        public int? JobstatusId { get; set; }
        public string JobDescription { get; set; }
        public bool? Approved { get; set; }
        public string Remarks { get; set; }
        public int? ExperienceTypeId { get; set; }

         

        public string LocationText { get; set; }
        public string RankText { get; set; }
        public string PromotionText { get; set; }
        public string JobStatusText { get; set; }
        public string ExperienceTypeText { get; set; }

        public String StartDateText { get; set; }
        public String EndDateText { get; set; }

        public string Duration { get; set; }
    }

    public class SearchPersonExperienceQueryHandler : IRequestHandler<SearchPersonExperienceQuery, List<SearchedPersonExperience>>
    {
        private readonly HCMContext _context;
        public SearchPersonExperienceQueryHandler(HCMContext context)
        {
            _context = context;
        }
        public async Task<List<SearchedPersonExperience>> Handle(SearchPersonExperienceQuery request, CancellationToken cancellationToken)
        {
            List<SearchedPersonExperience> result = new List<SearchedPersonExperience>();

            // Search based on PersonExperienceRecord
            if (request.Id !=null && request.Id != default(decimal))
            {
                result = await (from pe in _context.Experience
                                join l in _context.Location on pe.LocationId equals l.Id into peL
                                from resultLocation in peL.DefaultIfEmpty()

                                join r in _context.Rank on pe.RankId equals r.Id into peR
                                from resultRank in peR.DefaultIfEmpty()

                                join js in _context.JobStatus on pe.JobstatusId equals js.Id into peJs
                                from resultJobStatus in peJs.DefaultIfEmpty()

                                join exT in _context.ExperienceType on pe.ExperienceTypeId equals exT.Id into peExT
                                from resultExperienceType in peExT.DefaultIfEmpty()

                                join pr in _context.Result on pe.PromotionId equals pr.Id into promotion
                                from resultpromotion in promotion.DefaultIfEmpty()

                                where pe.Id == request.Id
                                select new SearchedPersonExperience
                                {

                                    Designation  = pe.Designation,
                                    Id = pe.Id,
                                    PersonId = pe.PersonId,
                                    Organization = pe.Organization,
                                    RequestNo = pe.RequestNo,
                                    LocationId = pe.LocationId,
                                    DocumentNo = pe.DocumentNo,
                                    RankId = pe.RankId,
                                    PromotionId = pe.PromotionId,
                                    StartDate = pe.StartDate,
                                    EndDate = pe.EndDate,
                                    ContactInfo = pe.ContactInfo,
                                    JobstatusId = pe.JobstatusId,
                                    JobDescription = pe.JobDescription,
                                    Approved = pe.Approved,
                                    Remarks = pe.Remarks,
                                    ExperienceTypeId = pe.ExperienceTypeId,
                                    LocationText =resultLocation.Dari,
                                    RankText =  resultRank.Name,
                                    PromotionText = resultpromotion.Dari,
                                    JobStatusText =resultJobStatus.Name,
                                    ExperienceTypeText = resultExperienceType.Dari, 
                                    StartDateText = PersianLibrary.PersianDate.GetFormatedString(pe.StartDate.Value),
                                    EndDateText = PersianLibrary.PersianDate.GetFormatedString(pe.EndDate.Value)

                                }).OrderByDescending(e => e.EndDate).ToListAsync(cancellationToken);
            }

            // Serach based on PersonID
            else if(request.PersonId != null)
            {
                result = await (from pe in _context.Experience
                                join l in _context.Location on pe.LocationId equals l.Id into peL
                                from resultLocation in peL.DefaultIfEmpty()

                                join r in _context.Rank on pe.RankId equals r.Id into peR
                                from resultRank in peR.DefaultIfEmpty()

                                join js in _context.JobStatus on pe.JobstatusId equals js.Id into peJs
                                from resultJobStatus in peJs.DefaultIfEmpty()

                                join exT in _context.ExperienceType on pe.ExperienceTypeId equals exT.Id into peExT
                                from resultExperienceType in peExT.DefaultIfEmpty()

                                join pr in _context.Result on pe.PromotionId equals pr.Id into promotion
                                from resultpromotion in promotion.DefaultIfEmpty()


                                where pe.PersonId == request.PersonId
                                select new SearchedPersonExperience
                                {

                                    Designation = pe.Designation,
                                    Id = pe.Id,
                                    PersonId = pe.PersonId,
                                    Organization = pe.Organization,
                                    RequestNo = pe.RequestNo,
                                    LocationId = pe.LocationId,
                                    DocumentNo = pe.DocumentNo,
                                    RankId = pe.RankId,
                                    PromotionId = pe.PromotionId,
                                    StartDate = pe.StartDate,
                                    EndDate = pe.EndDate,
                                    ContactInfo = pe.ContactInfo,
                                    JobstatusId = pe.JobstatusId,
                                    JobDescription = pe.JobDescription,
                                    Approved = pe.Approved,
                                    Remarks = pe.Remarks,
                                    ExperienceTypeId = pe.ExperienceTypeId,
                                    LocationText = resultLocation.Dari,
                                    RankText = resultRank.Name,
                                    PromotionText = resultpromotion.Dari,
                                    JobStatusText = resultJobStatus.Name,
                                    ExperienceTypeText = resultExperienceType.Dari, 
                                    StartDateText = PersianLibrary.PersianDate.GetFormatedString(pe.StartDate.Value),
                                    EndDateText = PersianLibrary.PersianDate.GetFormatedString(pe.EndDate.Value)

                                }).OrderByDescending(e => e.EndDate).ToListAsync(cancellationToken);
            }

            


            return result;
        }
    }
}
