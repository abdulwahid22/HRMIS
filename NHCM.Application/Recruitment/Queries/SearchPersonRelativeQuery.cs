using MediatR;
using NHCM.Application.Common;
using NHCM.Application.Recruitment.Models;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NHCM.Application.Recruitment.Queries
{
    public class SearchPersonRelativeQuery : IRequest<List<SearchedPersonRelative>>
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }

        public decimal? Id { get; set; }
        
        public decimal? PersonId { get; set; }
        public int? RelationShipId { get; set; }
        public string NidNo { get; set; }
        public string Profession { get; set; }
        public string Address { get; set; }
        public string ContactInfo { get; set; }
        public string EmailAddress { get; set; }
        public string Village { get; set; }
        public int? LocationId { get; set; }
        public string Remark { get; set; }

        
    }
    public class SearchPersonRelativeQueryHandler : IRequestHandler<SearchPersonRelativeQuery, List<SearchedPersonRelative>>
    {
        private HCMContext _context;
        public SearchPersonRelativeQueryHandler(HCMContext context) { _context = context; }
        public async Task<List<SearchedPersonRelative>> Handle(SearchPersonRelativeQuery request, CancellationToken cancellationToken)
        {
            PersonCommon common = new PersonCommon(_context);

            return await common.SearchPersonRelative(request, cancellationToken);
            
        }
        
    }
}
