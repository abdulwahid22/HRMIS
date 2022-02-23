using MediatR;
using Microsoft.EntityFrameworkCore;
using NHCM.Application.Common;
using NHCM.Application.Recruitment.Models;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace NHCM.Application.Recruitment.Commands
{
    

    public class SavePersonSkillsCommand : IRequest<List<SearchedPersonSkill>>
    {

        public decimal? Id { get; set; }
        public decimal PersonId { get; set; }
        public int LanguageId { get; set; }
        public short ExpertiseId { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public int? Locationid { get; set; }
        public string Remarks { get; set; }
        public int? CertificationId { get; set; }
        public string CertifiedFrom { get; set; }
        public DateTime? CertificationDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
      
    }
    public class SavePersonSkillsCommandHandler : IRequestHandler<SavePersonSkillsCommand, List<SearchedPersonSkill>>
    {


        private readonly HCMContext _context;
        private readonly IMediator _mediator;

        public SavePersonSkillsCommandHandler(HCMContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;

        } 

        public async Task<List<SearchedPersonSkill>> Handle(SavePersonSkillsCommand request, CancellationToken cancellationToken)
        {

            
            List<SearchedPersonSkill> result = new List<SearchedPersonSkill>();
           

            if (request.Id == null || request.Id == default(decimal))
            {

                using (_context)
                {
                    PersonSkill personSkill = new PersonSkill()
                    {

                        PersonId = request.PersonId,
                        LanguageId = request.LanguageId,
                        ExpertiseId = request.ExpertiseId,
                        ModifiedOn = request.ModifiedOn,
                        ModifiedBy = request.ModifiedBy,
                        ReferenceNo = request.ReferenceNo,
                        CreatedOn = request.CreatedOn,
                        CreatedBy = request.CreatedBy,
                        Locationid = request.Locationid,
                        Remarks = request.Remarks,
                        CertificationId = request.CertificationId,
                        CertifiedFrom = request.CertifiedFrom,
                        CertificationDate = request.CertificationDate,
                        StartDate = request.StartDate,
                        EndDate = request.EndDate
                         
                     };

                    _context.PersonSkill.Add(personSkill);
                    await _context.SaveChangesAsync(cancellationToken);

                    
                    result = await _mediator.Send(new Queries.SearchPersonSkillQuery() { Id = personSkill.Id });
                      
                    
                }
            }
            else
            {
                using (_context)
                {
                    PersonSkill toUpdateRecord = await(from ps in _context.PersonSkill
                                                          where ps.Id == request.Id
                                                          select ps).SingleOrDefaultAsync();

                                 toUpdateRecord.PersonId = request.PersonId;
                                 toUpdateRecord.LanguageId = request.LanguageId;
                                 toUpdateRecord.ExpertiseId = request.ExpertiseId;
                                 toUpdateRecord.ModifiedOn = request.ModifiedOn;
                                 toUpdateRecord.ModifiedBy = request.ModifiedBy;
                                 toUpdateRecord.ReferenceNo = request.ReferenceNo;
                                 toUpdateRecord.CreatedOn = request.CreatedOn;
                                 toUpdateRecord.CreatedBy = request.CreatedBy;
                                 toUpdateRecord.Locationid = request.Locationid;
                                 toUpdateRecord.Remarks = request.Remarks;
                                 toUpdateRecord.CertificationId = request.CertificationId;
                                 toUpdateRecord.CertifiedFrom = request.CertifiedFrom;
                                 toUpdateRecord.CertificationDate = request.CertificationDate;
                                 toUpdateRecord.StartDate = request.StartDate;
                                 toUpdateRecord.EndDate = request.EndDate; 
                    await _context.SaveChangesAsync(cancellationToken);

                    result = await _mediator.Send(new Queries.SearchPersonSkillQuery() { Id = toUpdateRecord.Id });
                } 
            }

            return result;
        }
    }
}
