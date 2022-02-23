using MediatR;
using Microsoft.EntityFrameworkCore;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NHCM.Application.Lookup.Queries
{
    public class GetAssetTypeQuery : IRequest<List<AssetType>>
    {
        public short? ID { get; set; }
    }
    public class GetAssetTypeQueryHandler : IRequestHandler<GetAssetTypeQuery, List<AssetType>>
    {
        private readonly HCMContext _dbContext;
        public GetAssetTypeQueryHandler(HCMContext context)
        {
            _dbContext = context;
        }
        public async Task<List<AssetType>> Handle(GetAssetTypeQuery request, CancellationToken cancellationToken)
        {
            List<AssetType> list = new List<AssetType>();

            if (request.ID == null || request.ID ==default(short))
            {

                
                list = await _dbContext.AssetType.ToListAsync(cancellationToken);

                return list;


            }
            else
            {
               
                list = await _dbContext.AssetType.Where(b => b.Id == request.ID).ToListAsync(cancellationToken);
                return list;

            }
        }
    }
}
