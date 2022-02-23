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
    public class SavePersonAssetCommand : IRequest<List<SearchedPersonAsset>>
    {
        public decimal? Id { get; set; }
        public decimal PersonId { get; set; }
        public short AssetTypeId { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public string Description { get; set; }
        public decimal? Value { get; set; }

    }



    public class SavePersonAssetCommandHandler : IRequestHandler<SavePersonAssetCommand, List<SearchedPersonAsset>>
    {


        private readonly HCMContext _context;
        private readonly IMediator _mediator;
        private readonly ICurrentUser _currentUser;
        public SavePersonAssetCommandHandler(HCMContext context, IMediator mediator, ICurrentUser currentUser)
        {
            _context = context;
            _mediator = mediator;
            _currentUser = currentUser;
        }


        public async Task<List<SearchedPersonAsset>> Handle(SavePersonAssetCommand request, CancellationToken cancellationToken)
        {
            List<SearchedPersonAsset> result = new List<SearchedPersonAsset>();

            if (request.Id == null || request.Id == default(decimal))
            {

                int CurrentUserId = await _currentUser.GetUserId();

                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        using (_context)
                        {
                            PersonAsset personAsset = new PersonAsset()
                            {
                                PersonId = request.PersonId,
                                AssetTypeId = request.AssetTypeId,
                                ModifiedOn = request.ModifiedOn,
                                ModifiedBy = request.ModifiedBy,
                                ReferenceNo = request.ReferenceNo,
                                CreatedOn = request.CreatedOn,
                                CreatedBy = request.CreatedBy,
                                Description = request.Description,
                                Value = request.Value
                            };
                            _context.PersonAsset.Add(personAsset);
                            await _context.SaveChangesAsync(CurrentUserId, cancellationToken);



                            result = await _mediator.Send(new Queries.SearchPersonAssetQuery() { Id = personAsset.Id });
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
                    PersonAsset toUpdateRecord = await (from ps in _context.PersonAsset
                                                        where ps.Id == request.Id
                                                        select ps).SingleOrDefaultAsync();

                    toUpdateRecord.PersonId = request.PersonId;
                    toUpdateRecord.AssetTypeId = request.AssetTypeId;
                    toUpdateRecord.ModifiedOn = request.ModifiedOn;
                    toUpdateRecord.ModifiedBy = request.ModifiedBy;
                    toUpdateRecord.ReferenceNo = request.ReferenceNo;
                    toUpdateRecord.CreatedOn = request.CreatedOn;
                    toUpdateRecord.CreatedBy = request.CreatedBy;
                    toUpdateRecord.Description = request.Description;
                    toUpdateRecord.Value = request.Value;

                    await _context.SaveChangesAsync(cancellationToken);

                    result = await _mediator.Send(new Queries.SearchPersonAssetQuery() { Id = toUpdateRecord.Id });

                }
            }

                return result;
            }
        }
    }
