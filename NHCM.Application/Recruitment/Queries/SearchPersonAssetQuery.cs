using MediatR;
using NHCM.Application.Common;
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
    public class SearchPersonAssetQuery : IRequest<List<SearchedPersonAsset>>
    {
        public decimal? Id { get; set; }
        public decimal? PersonId { get; set; }
        public short? AssetTypeId { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public string Description { get; set; }
        public decimal? Value { get; set; }


        public string AssetTypeIdText { get; set; }
    }

    public class SearchPersonAssetQueryHandler : IRequestHandler<SearchPersonAssetQuery, List<SearchedPersonAsset>>
    {
        private HCMContext _context;
        public SearchPersonAssetQueryHandler(HCMContext context) { _context = context; }

        public async Task<List<SearchedPersonAsset>> Handle(SearchPersonAssetQuery request, CancellationToken cancellationToken)
        {

            List<SearchedPersonAsset> result = new List<SearchedPersonAsset>();
            if (request.Id != null)
            {
                using (_context)
                {
                    result = await (from pa in _context.PersonAsset

                                    join AssetType in _context.AssetType on pa.AssetTypeId equals AssetType.Id into pat
                                    from resulpat in pat.DefaultIfEmpty()

                                    where pa.Id == request.Id
                                    select new SearchedPersonAsset
                                    { 
                                        Id=pa.Id,
                                        PersonId=pa.PersonId,
                                        AssetTypeId = pa.AssetTypeId,
                                        ModifiedOn = pa.ModifiedOn,
                                        ModifiedBy = pa.ModifiedBy,
                                        ReferenceNo = pa.ReferenceNo,
                                        CreatedOn = pa.CreatedOn,
                                        CreatedBy = pa.CreatedBy,
                                        Description = pa.Description,
                                        Value = pa.Value,
                                        
                                        AssetTypeText = resulpat.Name

                                    }).ToListAsync(cancellationToken);
                }
            }

            else if (request.PersonId != null)
            {

                using (_context)
                {
                    result = await (from pa in _context.PersonAsset

                                    join AssetType in _context.AssetType on pa.AssetTypeId equals AssetType.Id into pat
                                    from resulpat in pat.DefaultIfEmpty()

                                    where pa.PersonId == request.PersonId
                                    select new SearchedPersonAsset
                                    {
                                        Id = pa.Id,
                                        PersonId = pa.PersonId,
                                        AssetTypeId = pa.AssetTypeId,
                                        ModifiedOn = pa.ModifiedOn,
                                        ModifiedBy = pa.ModifiedBy,
                                        ReferenceNo = pa.ReferenceNo,
                                        CreatedOn = pa.CreatedOn,
                                        CreatedBy = pa.CreatedBy,
                                        Description = pa.Description,
                                        Value = pa.Value,
                                        AssetTypeText = resulpat.Name

                                    }).ToListAsync(cancellationToken);
                }
            }
            return result;

        }

    }
}
