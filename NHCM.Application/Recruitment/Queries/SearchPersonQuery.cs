using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using NHCM.Application.Recruitment.Models;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using NHCM.Persistence;
using NHCM.Application.Common;

namespace NHCM.Application.Recruitment.Queries
{
    public class SearchPersonQuery : IRequest<List<SearchedPersonModel>>
    {

        public decimal? Id { get; set; }
        public string Hrcode { get; set; }
        public int NoOfRecords { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public string FirstNameEng { get; set; }
        public string LastNameEng { get; set; }
        public string FatherNameEng { get; set; }
        public string GrandFatherNameEng { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public int? BirthLocationId { get; set; }
        public int? GenderId { get; set; }
        public short? MaritalStatusId { get; set; }
        public int? EthnicityId { get; set; }
        public short? ReligionId { get; set; }
        public int? BloodGroupId { get; set; }

        public int? DocumentTypeId { get; set; }
        public string PhotoPath { get; set; }
        public string Nid { get; set; }

        public int? OrganizationId { get; set; }

    }


    public class SearchPersonQueryHandler : IRequestHandler<SearchPersonQuery, List<SearchedPersonModel>>
    {

        private readonly HCMContext _context;
        //private readonly IMediator _mediator;

        public SearchPersonQueryHandler(HCMContext context/*, IMediator mediator*/)
        {
            _context = context;
            //_mediator = mediator;
        }

        public async Task<List<SearchedPersonModel>> Handle(SearchPersonQuery request, CancellationToken cancellationToken)
        {

            PersonCommon common = new PersonCommon(_context);
            List<SearchedPersonModel> result = new List<SearchedPersonModel>();
            result = await common.SearchPerson(request);

            return result;

        }
    }
}
