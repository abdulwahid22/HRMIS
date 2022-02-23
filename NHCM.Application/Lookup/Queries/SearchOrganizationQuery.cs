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
    public class SearchOrganizationQuery : IRequest<List<SearchedOrganizationModel>>
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Dari { get; set; }
        public string Pashto { get; set; }
        public short? OrgUnitTypeId { get; set; }
        public string Code { get; set; }
        public short? StatusId { get; set; }
        public int? Reference { get; set; }

        public string OrgUnitTypeText { get; set; }
    }

    public class SearchOrganizationQueryHandler : IRequestHandler<SearchOrganizationQuery, List<SearchedOrganizationModel>>
    {
        private HCMContext _context;
        public SearchOrganizationQueryHandler(HCMContext context)
        {
            _context = context;
        }
        public async Task<List<SearchedOrganizationModel>> Handle(SearchOrganizationQuery request, CancellationToken cancellationToken)
        {
            List<SearchedOrganizationModel> result = new List<SearchedOrganizationModel>();

            if (request.Id != null)
            {
                result = await(from org in _context.Organization          
                               join orut in _context.OrganizationType on org.OrgUnitTypeId equals orut.Id into OuOp
                               from resultOuOp in OuOp.DefaultIfEmpty() 
                               where org.Id == request.Id 
                               select new SearchedOrganizationModel
                               {
                                   Id = org.Id,
                                   Dari = org.Dari,
                                   Pashto = org.Pashto,
                                   Name = org.Name,
                                   OrgUnitTypeId = org.OrgUnitTypeId,
                                   OrgUnitTypeText = resultOuOp.Name,
                                   Code = org.Code,
                                   StatusId = org.StatusId

                               }).ToListAsync(cancellationToken);
            }
            else
            {
                result = await (from org in _context.Organization
                                join orut in _context.OrganizationType on org.OrgUnitTypeId equals orut.Id into OuOp
                                from resultOuOp in OuOp.DefaultIfEmpty() 
                                select new SearchedOrganizationModel
                                {
                                    Id = org.Id,
                                    Dari = org.Dari,
                                    Pashto = org.Pashto,
                                    Name = org.Name,
                                    OrgUnitTypeId = org.OrgUnitTypeId,
                                    OrgUnitTypeText = resultOuOp.Name,
                                    Code = org.Code,
                                    StatusId = org.StatusId

                                }).ToListAsync(cancellationToken);
            }
            return result;
        }
    }
}
