using MediatR;
using Microsoft.EntityFrameworkCore;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NHCM.Application.Organogram.Models;

namespace NHCM.Application.Lookup.Queries
{
    public class GetReportToQuery : IRequest<List<PositionType>>
    {
        public int? ID { get; set; }
        
    }

    public class GetReportToHandler : IRequestHandler<GetReportToQuery, List<PositionType>>
    {
        private readonly HCMContext _dbContext;
        public GetReportToHandler(HCMContext context)
        {
            _dbContext = context;
        }
        public async Task<List<PositionType>> Handle(GetReportToQuery request, CancellationToken cancellationToken)
        {
            List<PositionType> list = new List<PositionType>();

            if (request.ID != null)
            { 
                list = await (from orgp in _dbContext.OrgPosition
                                join pot in _dbContext.PositionType on orgp.PositionTypeId equals pot.Id into oppt
                                from resultoppt in oppt.DefaultIfEmpty() 
                                where orgp.Id == request.ID 
                                select new PositionType
                                {
                                    Id = resultoppt.Id,
                                    Name = resultoppt.Name
                                }).ToListAsync(cancellationToken);
                return list;
            } 
            else
            {
                list = await (from orgp in _dbContext.OrgPosition
                              join pot in _dbContext.PositionType on orgp.PositionTypeId equals pot.Id into oppt
                              from resultoppt in oppt.DefaultIfEmpty()
                              
                              select new PositionType
                              {
                                  Id = resultoppt.Id,
                                  Name = resultoppt.Name
                              }).ToListAsync(cancellationToken);
                return list;
            }


        }
    }

}
