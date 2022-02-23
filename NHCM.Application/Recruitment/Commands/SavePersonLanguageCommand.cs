using MediatR;
using Microsoft.EntityFrameworkCore;
using NHCM.Application.Common;
using NHCM.Application.Recruitment.Models;
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
    public class SavePersonLanguageCommand : IRequest<List<SearchedPersonLanguage>>
    {


        public decimal? Id { get; set; }
        public decimal PersonId { get; set; }
        public short LanguageId { get; set; }
        public short? ReadingExpertise { get; set; }
        public short? UnderstandingExpertise { get; set; }
        public short? WritingExpertise { get; set; }
        public short? SpeakingExpertise { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }

        
    }
    public class SavePersonLanguageCommandHandler : IRequestHandler<SavePersonLanguageCommand, List<SearchedPersonLanguage>>
    { 
        private readonly HCMContext _context;
        private readonly ICurrentUser _currentUser;
        public SavePersonLanguageCommandHandler(HCMContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        } 
        public async Task<List<SearchedPersonLanguage>> Handle(SavePersonLanguageCommand request, CancellationToken cancellationToken)
        {
            List<SearchedPersonLanguage> result = new List<SearchedPersonLanguage>();
            PersonCommon common = new PersonCommon(_context);

            if (request.Id == null || request.Id == default(decimal))
            {
                int CurrentUserId = await _currentUser.GetUserId();

                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        using (_context)
                        {
                            PersonLanguage personLanguage = new PersonLanguage()
                            {
                                PersonId = request.PersonId,
                                LanguageId = request.LanguageId,
                                ReadingExpertise = request.ReadingExpertise,
                                UnderstandingExpertise = request.UnderstandingExpertise,
                                WritingExpertise = request.WritingExpertise,
                                SpeakingExpertise = request.SpeakingExpertise,
                                ReferenceNo = request.ReferenceNo,
                                CreatedOn = request.CreatedOn,
                                CreatedBy = request.CreatedBy
                            };
                            _context.PersonLanguage.Add(personLanguage);
                            await _context.SaveChangesAsync(CurrentUserId, cancellationToken);
                            result = await common.SearchPersonLanguages(new Queries.SearchPersonLanguageQuery() { Id = personLanguage.Id }, cancellationToken);
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
                    PersonLanguage toUpdateRecord = await (from pl in _context.PersonLanguage
                                                           where pl.Id == request.Id
                                                           select pl).SingleOrDefaultAsync();

                    toUpdateRecord.LanguageId = request.LanguageId;
                    toUpdateRecord.ReadingExpertise = request.ReadingExpertise;
                    toUpdateRecord.UnderstandingExpertise = request.UnderstandingExpertise;
                    toUpdateRecord.WritingExpertise = request.WritingExpertise;
                    toUpdateRecord.SpeakingExpertise = request.SpeakingExpertise;



                    await _context.SaveChangesAsync(cancellationToken);

                    //result = await _context.PersonLanguage.Where(pl => pl.Id == toUpdateRecord.Id).ToListAsync(cancellationToken);

                    result = await common.SearchPersonLanguages(new Queries.SearchPersonLanguageQuery() { Id = toUpdateRecord.Id }, cancellationToken);

                }

            }

            return result;
        }
    }
}
