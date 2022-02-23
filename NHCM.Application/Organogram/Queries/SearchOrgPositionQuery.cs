using MediatR;
using NHCM.Application.Organogram.Models;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NHCM.Application.Organogram.Queries
{
    public class SearchOrgPositionQuery : IRequest<List<SearchedOrgPosition>>
    {
        public int PositionTypeId { get; set; }
        public int OrgUnitTypeId { get; set; }
        public int? ParentId { get; set; }
        public int? Id { get; set; }
        public short RankId { get; set; }
        public bool Children { get; set; }
    }


    public class SearchOrgPositionQueryHandler : IRequestHandler<SearchOrgPositionQuery, List<SearchedOrgPosition>>
    {
        private HCMContext _context;
        public SearchOrgPositionQueryHandler(HCMContext context)
        {
            _context = context;
        }


        public async Task<List<SearchedOrgPosition>> Handle(SearchOrgPositionQuery request, CancellationToken cancellationToken)
        {
            List<SearchedOrgPosition> result = new List<SearchedOrgPosition>();

            List<SearchedOrgPosition> data = await (from OP in _context.OrgPosition
                                                    join PT in _context.PositionType on OP.PositionTypeId equals PT.Id into OPTs
                                                    from resultOrgPositions in OPTs.DefaultIfEmpty()
                                                    join OUT in _context.OrgUnitType on OP.OrgUnitTypeId equals OUT.Id into OUTs
                                                    from resultOrgUnits in OUTs.DefaultIfEmpty()
                                                    join R in _context.Rank on OP.RankId equals R.Id into Rs
                                                    from resultRanks in Rs.DefaultIfEmpty()
                                                    select new SearchedOrgPosition
                                                    {
                                                        Id = OP.Id,
                                                        PositionTypeId = OP.PositionTypeId,
                                                        OrgUnitTypeId = OP.OrgUnitTypeId,
                                                        ParentId = OP.ParentId,
                                                        RankId = OP.RankId,
                                                        RankText = resultRanks.Name,
                                                        PositionTypeText = resultOrgPositions.Name,
                                                        OrgUnitText = resultOrgUnits.Name

                                                    }).ToListAsync(cancellationToken);

            if (request.Id != null)
            {
                if (!request.Children)
                    result = data.Where(c => c.Id == request.Id).ToList();
                else
                    result = data.Where(c => c.ParentId == request.Id).ToList();
            }
            else
                result = data;
            return result;
        }
    }
}
