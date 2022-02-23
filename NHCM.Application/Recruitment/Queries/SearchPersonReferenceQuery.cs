using MediatR;
using NHCM.Application.Recruitment.Models;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace NHCM.Application.Recruitment.Queries
{
  public  class SearchPersonReferenceQuery : IRequest<List<SearchedPersonReference>>
    {
        public decimal? Id { get; set; }
        public decimal? PersonId { get; set; }
        
        public string ReferenceNo { get; set; }
         
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public string Occupation { get; set; }
        public string Organization { get; set; }
        public string TelephoneNo { get; set; }
        public string District { get; set; }
        public int? LocationId { get; set; }
        public string RelationShip { get; set; }
        public short? ReferenceTypeId { get; set; }

        public string Remark { get; set; }

         
    }


    public class SearchPersonReferenceQueryHandler : IRequestHandler<SearchPersonReferenceQuery, List<SearchedPersonReference>>
    {

        private HCMContext _context;
        public SearchPersonReferenceQueryHandler(HCMContext context)
        {
            _context = context;
        }
        public async Task<List<SearchedPersonReference>> Handle(SearchPersonReferenceQuery request, CancellationToken cancellationToken)
        {
            List<SearchedPersonReference> result = new List<SearchedPersonReference>();


            if (request.Id != null)
            {

                result = await (from pr in _context.Reference
                                join l in _context.Location on pr.LocationId equals l.Id into prL
                                from resultLocation in prL.DefaultIfEmpty()

                                join rt in _context.ReferenceType on pr.ReferenceTypeId equals rt.Id into prRt
                                from resultReferenceType in prRt.DefaultIfEmpty()


                                where pr.Id == request.Id
                                select new SearchedPersonReference
                                {
                                    Id = pr.Id,
                                    PersonId = pr.PersonId,
                                    ReferenceNo = pr.ReferenceNo,
                                    FirstName = pr.FirstName,
                                    LastName = pr.LastName,
                                    FatherName = pr.FatherName,
                                    GrandFatherName = pr.GrandFatherName,
                                    Occupation = pr.Occupation,
                                    Organization = pr.Organization,
                                    TelephoneNo = pr.TelephoneNo,
                                    District = pr.District,
                                    LocationId = pr.LocationId,
                                    RelationShip = pr.RelationShip,
                                    ReferenceTypeId = pr.ReferenceTypeId,
                                    Remark = pr.Remark,

                                    ReferenceTypeText = resultReferenceType.Name,
                                    LocationText = resultLocation.Dari



                                }).ToListAsync(cancellationToken);
            }


            else if (request.PersonId != null)
            {
                result = await (from pr in _context.Reference
                                join l in _context.Location on pr.LocationId equals l.Id into prL
                                from resultLocation in prL.DefaultIfEmpty()

                                join rt in _context.ReferenceType on pr.ReferenceTypeId equals rt.Id into prRt
                                from resultReferenceType in prRt.DefaultIfEmpty()


                                where pr.PersonId == request.PersonId
                                select new SearchedPersonReference
                                { 
                                    Id = pr.Id,
                                    PersonId = pr.PersonId, 
                                    ReferenceNo = pr.ReferenceNo,  
                                    FirstName = pr.FirstName,
                                    LastName = pr.LastName,
                                    FatherName = pr.FatherName,
                                    GrandFatherName = pr.GrandFatherName,
                                    Occupation = pr.Occupation,
                                    Organization = pr.Organization,
                                    TelephoneNo = pr.TelephoneNo,
                                    District = pr.District,
                                    LocationId = pr.LocationId,
                                    RelationShip = pr.RelationShip,
                                    ReferenceTypeId = pr.ReferenceTypeId,
                                    Remark = pr.Remark,

                                    ReferenceTypeText = resultReferenceType.Name,
                                    LocationText = resultLocation.Dari
                                     


                                }).ToListAsync(cancellationToken);
            }






            return result;
        }
    }
}
