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
using NHCM.Application.Employment.Models;
using NHCM.Application.Organogram.Models;
using NHCM.Application.Organogram.Queries;
using NHCM.Domain.Entities;

namespace NHCM.Application.Employment.Queries
{
    public class SearchZoneEmployeeQuery : IRequest<List<SearchedZoneModel>>
    {
        public int? ID { get; set; }
     
        public decimal? PersonID { get; set; }
        public decimal? ZoneID { get; set; }
        public int? Code { get; set; }

        public string FirstName { get; set; }
        public string ZoneName { get; set; }
    }


    public class SearchZoneEmployeeQueryHandler : IRequestHandler<SearchZoneEmployeeQuery, List<SearchedZoneModel>>
    {
        private readonly HCMContext _context;
        private readonly IMediator _mediator;

        public SearchZoneEmployeeQueryHandler(HCMContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<List<SearchedZoneModel>> Handle(SearchZoneEmployeeQuery request, CancellationToken cancellationToken)
        {
            List<SearchedZoneModel> result = new List<SearchedZoneModel>();

            if (request.ID != null)
            {
                result = await (from zem in _context.ZoneEmployees

                                join p in _context.Person on zem.PersonID equals p.Id into pe
                                from rpw in pe.DefaultIfEmpty()
                                join zo in _context.Zones on zem.ZoneID equals zo.ID into Zns
                                from rops in Zns.DefaultIfEmpty()

                                where zem.ID == request.ID

                                select new SearchedZoneModel
                                {
                                    ID =zem.ID,
                                    PersonID = rpw.Id,
                                    ZoneID = zem.ZoneID,
                                    FirstName = rpw.FirstName + "  " + "فرزند" + " " + rpw.FatherName,
                                    ZoneName = rops.Name

                                }).ToListAsync(cancellationToken);
            }

            else if (request.PersonID != default(decimal) && request.PersonID !=null)
            {
                result = await (from zem in _context.ZoneEmployees
                                join p in _context.Person on zem.PersonID equals p.Id into pe
                                from rpw in pe.DefaultIfEmpty()
                                join zo in _context.Zones on zem.ZoneID equals zo.ID into Zns
                                from rops in Zns.DefaultIfEmpty()
                                where zem.PersonID == request.PersonID
                                select new SearchedZoneModel
                                {
                                    ID = zem.ID,
                                    PersonID = rpw.Id,
                                    ZoneID = zem.ZoneID,

                                    FirstName = rpw.FirstName +"  " + "فرزند" + " "+rpw.FatherName,
                                    ZoneName = rops.Name
                                 
                                }).ToListAsync(cancellationToken);
            }
            else
            {
                result = await (from zem in _context.ZoneEmployees
                                join p in _context.Person on zem.PersonID equals p.Id into pe
                                from rpw in pe.DefaultIfEmpty()
                                join zo in _context.Zones on zem.ZoneID equals zo.ID into Zns
                                from rops in Zns.DefaultIfEmpty()
                                select new SearchedZoneModel
                                {
                                    ID = zem.ID,
                                    PersonID = rpw.Id,
                                    ZoneID = zem.ZoneID,

                                    FirstName = rpw.FirstName + "  " + "فرزند" + " " + rpw.FatherName,
                                    ZoneName = rops.Name
                                 
                                }).ToListAsync(cancellationToken);
            }
            return result;
        }
    }
}
