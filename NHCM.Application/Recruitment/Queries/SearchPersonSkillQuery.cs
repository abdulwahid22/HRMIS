using MediatR;
using NHCM.Application.Recruitment.Models;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NHCM.Application.Recruitment.Queries
{
    public class SearchPersonSkillQuery : IRequest<List<SearchedPersonSkill>>
    {
        public decimal? Id { get; set; }
        public decimal? PersonId { get; set; }
        public int? LanguageId { get; set; }
        public short ExpertiseId { get; set; } 
        public int? Locationid { get; set; } 
        public int? CertificationId { get; set; }
        public string CertifiedFrom { get; set; }
        public DateTime? CertificationDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string ExpertiseText { get; set; }
        public string CertificationText { get; set; } 
        public String StartDateText { get; set; }
        public String EndDateText { get; set; }

    }

    public class SearchPersonSkillQueryHandler : IRequestHandler<SearchPersonSkillQuery, List<SearchedPersonSkill>>
    {
        private HCMContext _context;
        public SearchPersonSkillQueryHandler(HCMContext context) { _context = context; }
        public async Task<List<SearchedPersonSkill>> Handle(SearchPersonSkillQuery request, CancellationToken cancellationToken)
        {

            List<SearchedPersonSkill> result = new List<SearchedPersonSkill>();
            if (request.Id != null)
            { 
                using (_context)
                {
                    result = await (from ps in _context.PersonSkill

                                    join cert in _context.Certification on ps.CertificationId equals cert.Id into psCert
                                    from resultCert in psCert.DefaultIfEmpty()

                                    join experties in _context.Expertise on ps.ExpertiseId equals experties.Id into se
                                    from resultse in se.DefaultIfEmpty()



                                    where ps.Id == request.Id
                                    select new SearchedPersonSkill
                                    {
                                        Id = ps.Id,
                                        PersonId = ps.PersonId,
                                        //LanguageId = ps.LanguageId,
                                        ExpertiseId = ps.ExpertiseId,
                                        Locationid = ps.Locationid,
                                        
                                        CertificationId = ps.CertificationId,
                                        CertifiedFrom = ps.CertifiedFrom,
                                        CertificationDate = ps.CertificationDate,
                                        StartDate = ps.StartDate,
                                        EndDate = ps.EndDate,
                                        ExpertiseText = resultse.Name, 
                                        CertificationText = resultCert.Name, 
                                        Remarks=ps.Remarks,
                                        

                                        StartDateText = PersianLibrary.PersianDate.GetFormatedString(ps.StartDate.Value),
                                        EndDateText = PersianLibrary.PersianDate.GetFormatedString(ps.EndDate.Value)


                                    }).OrderBy(s => s.EndDate).ToListAsync(cancellationToken);
                }
            }

            else if (request.PersonId != null)
            {

                using (_context)
                {
                    result = await (from ps in _context.PersonSkill

                                    join cert in _context.Certification on ps.CertificationId equals cert.Id into psCert
                                    from resultCert in psCert.DefaultIfEmpty()

                                    join experties in _context.Expertise on ps.ExpertiseId equals experties.Id into se
                                    from resultse in se.DefaultIfEmpty()



                                    where ps.PersonId == request.PersonId
                                    select new SearchedPersonSkill
                                    {
                                        Id = ps.Id,
                                        PersonId = ps.PersonId,
                                       // LanguageId = ps.LanguageId,
                                        ExpertiseId = ps.ExpertiseId,
                                        Locationid = ps.Locationid,
                                        CertificationId = ps.CertificationId,
                                        CertifiedFrom = ps.CertifiedFrom,
                                        CertificationDate = ps.CertificationDate,
                                        StartDate = ps.StartDate,
                                        EndDate = ps.EndDate,
                                        Remarks=ps.Remarks,
                                        ExpertiseText = resultse.Name, 
                                        CertificationText = resultCert.Name,

                                        StartDateText = PersianLibrary.PersianDate.GetFormatedString(ps.StartDate.Value),
                                        EndDateText = PersianLibrary.PersianDate.GetFormatedString(ps.EndDate.Value)


                                    }).OrderBy(s => s.EndDate).ToListAsync(cancellationToken);
                } 
            } 
            return result ;
        } 
    }
}
