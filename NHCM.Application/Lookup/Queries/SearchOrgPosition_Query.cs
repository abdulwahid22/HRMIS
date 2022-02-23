using MediatR;
using NHCM.Application.Lookup.Models;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NHCM.Application.Lookup.Queries
{
    public class SearchOrgPosition_Query : IRequest<List<SearchedOrgPositionModel>>
    {
        public int? Id { get; set; }
        public int? PositionTypeId { get; set; }
        public int? OrgUnitTypeId { get; set; }
        public int? ParentId { get; set; }
        public short? RankId { get; set; }
    }

    public class SearchOrgPosition_QueryHandler : IRequestHandler<SearchOrgPosition_Query, List<SearchedOrgPositionModel>>
    {
        private HCMContext _context;
        public SearchOrgPosition_QueryHandler(HCMContext context)
        {
            _context = context;
        }
        public async Task<List<SearchedOrgPositionModel>> Handle(SearchOrgPosition_Query request, CancellationToken cancellationToken)
        {
            List<SearchedOrgPositionModel> result = new List<SearchedOrgPositionModel>();

            if (request.Id != null)
            {
                result = await (from orgpo in _context.OrgPosition
                                join pot in _context.PositionType on orgpo.PositionTypeId equals pot.Id into OpPt
                                from resultOpPt in OpPt.DefaultIfEmpty()

                                join orgptparent in _context.OrgPosition on orgpo.Id equals orgptparent.Id into OpParent
                                from resultOpParent in OpParent.DefaultIfEmpty()

                                join orut in _context.OrgUnitType on orgpo.OrgUnitTypeId equals orut.Id into OuOp
                                from resultOuOp in OuOp.DefaultIfEmpty()

                                join rank in _context.Rank on orgpo.RankId equals rank.Id into Opra
                                from resultOpra in Opra.DefaultIfEmpty()

                                where orgpo.Id == request.Id

                                select new SearchedOrgPositionModel
                                {
                                    Id = orgpo.Id,
                                    PositionTypeId = orgpo.PositionTypeId,
                                    OrgUnitTypeId = orgpo.OrgUnitTypeId,
                                    ParentId = orgpo.ParentId,
                                    RankId = orgpo.RankId,

                                    PositionTypeText = resultOpPt.Name,
                                    OrgUnitTypeText = resultOuOp.Name,
                                    ParentText = resultOpPt.Name,
                                    RankText = resultOpra.Name

                                }).ToListAsync(cancellationToken);
            }

            else if (request.PositionTypeId != null)
            {
                result = await (from orgpo in _context.OrgPosition
                                join pot in _context.PositionType on orgpo.PositionTypeId equals pot.Id into OpPt
                                from resultOpPt in OpPt.DefaultIfEmpty()

                                join orgptparent in _context.OrgPosition on orgpo.Id equals orgptparent.Id into OpParent
                                from resultOpParent in OpParent.DefaultIfEmpty()

                                join orut in _context.OrgUnitType on orgpo.OrgUnitTypeId equals orut.Id into OuOp
                                from resultOuOp in OuOp.DefaultIfEmpty()

                                join rank in _context.Rank on orgpo.RankId equals rank.Id into Opra
                                from resultOpra in Opra.DefaultIfEmpty()

                                where orgpo.PositionTypeId == request.PositionTypeId

                                select new SearchedOrgPositionModel
                                {
                                    Id = orgpo.Id,
                                    PositionTypeId = orgpo.PositionTypeId,
                                    OrgUnitTypeId = orgpo.OrgUnitTypeId,
                                    ParentId = orgpo.ParentId,
                                    RankId = orgpo.RankId,

                                    PositionTypeText = resultOpPt.Name,
                                    OrgUnitTypeText = resultOuOp.Name,
                                    ParentText = resultOpPt.Name,
                                    RankText = resultOpra.Name

                                }).ToListAsync(cancellationToken);
            }
            else if (request.PositionTypeId != null && request.OrgUnitTypeId != null)
            {
                result = await (from orgpo in _context.OrgPosition
                                
                                where orgpo.PositionTypeId == request.PositionTypeId && orgpo.OrgUnitTypeId == request.OrgUnitTypeId

                                select new SearchedOrgPositionModel
                                {
                                    Id = orgpo.Id,
                                    PositionTypeId = orgpo.PositionTypeId,
                                    OrgUnitTypeId = orgpo.OrgUnitTypeId,
                                    ParentId = orgpo.ParentId,
                                    RankId = orgpo.RankId,
                                      
                                }).ToListAsync(cancellationToken);
            }
            else
            {
                result = await (from orgpo in _context.OrgPosition
                                join pot in _context.PositionType on orgpo.PositionTypeId equals pot.Id into OpPt
                                from resultOpPt in OpPt.DefaultIfEmpty()
                                join orgptparent in _context.PositionType on orgpo.ParentId equals orgptparent.Id into OpParent
                                from resultOpParent in OpParent.DefaultIfEmpty()
                                join orut in _context.OrgUnitType on orgpo.OrgUnitTypeId equals orut.Id into OuOp
                                from resultOuOp in OuOp.DefaultIfEmpty()
                                join rank in _context.Rank on orgpo.RankId equals rank.Id into Opra
                                from resultOpra in Opra.DefaultIfEmpty()

                                select new SearchedOrgPositionModel
                                {
                                    Id = orgpo.Id,
                                    PositionTypeId = orgpo.PositionTypeId,
                                    OrgUnitTypeId = orgpo.OrgUnitTypeId,
                                    ParentId = orgpo.ParentId,
                                    RankId = orgpo.RankId,

                                    PositionTypeText = resultOpPt.Name,
                                    OrgUnitTypeText = resultOuOp.Name,
                                    ParentText = resultOpParent.Name,
                                    RankText = resultOpra.Name

                                }).ToListAsync(cancellationToken);
            }
            return result;
        }
    }
}
