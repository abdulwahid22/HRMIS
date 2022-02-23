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
    public class SearchPersonAddressQuery : IRequest<List<SearchedPersonAdress>>
    { 
        public decimal? Id { get; set; }
        public decimal? PersonId { get; set; }

        public short? AddressTypeId { get; set; }
        public int? LocationId { get; set; }
        public int? DistrictId { get; set; }
        public string Village { get; set; }
        public string Address1 { get; set; } //change it to Address also in database
        public string StreetNo { get; set; }
        public string HouseNo { get; set; }
        public string Phone { get; set; }
        public string EmailAddress { get; set; }
        public string Mobile { get; set; }
        public bool? IsActive { get; set; }
        public string Paddress { get; set; }
        public int? ClocationId { get; set; }
        public int? CdistrictId { get; set; }
        public string Cvillage { get; set; }
         
    }
     
    public class SearchPersonAddressQueryHandler : IRequestHandler<SearchPersonAddressQuery, List<SearchedPersonAdress>>
    {

        private HCMContext _context;
        public SearchPersonAddressQueryHandler(HCMContext context)
        {
            _context = context;
        }
        public async Task<List<SearchedPersonAdress>> Handle(SearchPersonAddressQuery request, CancellationToken cancellationToken)
        {
            List<SearchedPersonAdress> result = new List<SearchedPersonAdress>();


            if (request.Id != null)
            {
                 
                result = await (from ad in _context.Address
                                join l in _context.Location on ad.LocationId equals l.Id into adL
                                from resultLocation in adL.DefaultIfEmpty()

                                join di in _context.District on ad.DistrictId equals di.Id into adD
                                from resultDistrict in adD.DefaultIfEmpty()

                                join lC in _context.Location on ad.ClocationId equals lC.Id into adLC
                                from resultCLocation in adLC.DefaultIfEmpty()
                                
                                join dC in _context.District on ad.CdistrictId equals dC.Id into adDC
                                from resultCdistrict in adDC.DefaultIfEmpty()

                                where ad.Id == request.Id
                                select new SearchedPersonAdress
                                {


                                    Id = ad.Id,
                                    PersonId = ad.PersonId,
                                    ModifiedOn = ad.ModifiedOn,
                                    ModifiedBy = ad.ModifiedBy,
                                    ReferenceNo = ad.ReferenceNo,
                                    CreatedOn = ad.CreatedOn,
                                    CreatedBy = ad.CreatedBy,
                                    AddressTypeId = ad.AddressTypeId,
                                    LocationId = ad.LocationId,
                                    DistrictId = ad.DistrictId,
                                    Village = ad.Village,
                                    Address1 = ad.Address1,  //change it to Address also in database
                                    StreetNo = ad.StreetNo,
                                    HouseNo = ad.HouseNo,
                                    Phone = ad.Phone,
                                    EmailAddress = ad.EmailAddress,
                                    Mobile = ad.Mobile,
                                    IsActive = ad.IsActive,
                                    Paddress = ad.Paddress,
                                    ClocationId = ad.ClocationId,
                                    CdistrictId = ad.CdistrictId,
                                    Cvillage = ad.Cvillage,


                                    LocationText =resultLocation.Name,
                                    //AdressTypeText = string.Empty,
                                    DistrictText =resultDistrict.Name,
                                    ClocationText = resultCLocation.Name,
                                    CdistrictText = resultCdistrict.Name
                                     
                                }).ToListAsync(cancellationToken);
            }


            else if (request.PersonId != null)
            {
                result = await (from ad in _context.Address
                                join l in _context.Location on ad.LocationId equals l.Id into adL
                                from resultLocation in adL.DefaultIfEmpty()

                                join di in _context.District on ad.DistrictId equals di.Id into adD
                                from resultDistrict in adD.DefaultIfEmpty()

                                join lC in _context.Location on ad.ClocationId equals lC.Id into adLC
                                from resultCLocation in adLC.DefaultIfEmpty()

                                join dC in _context.District on ad.CdistrictId equals dC.Id into adDC
                                from resultCdistrict in adDC.DefaultIfEmpty()

                                where ad.PersonId == request.PersonId
                                select new SearchedPersonAdress
                                {


                                    Id = ad.Id,
                                    PersonId = ad.PersonId,
                                    ModifiedOn = ad.ModifiedOn,
                                    ModifiedBy = ad.ModifiedBy,
                                    ReferenceNo = ad.ReferenceNo,
                                    CreatedOn = ad.CreatedOn,
                                    CreatedBy = ad.CreatedBy,
                                    AddressTypeId = ad.AddressTypeId,
                                    LocationId = ad.LocationId,
                                    DistrictId = ad.DistrictId,
                                    Village = ad.Village,
                                    Address1 = ad.Address1, //change it to Address also in database
                                    StreetNo = ad.StreetNo,
                                    HouseNo = ad.HouseNo,
                                    Phone = ad.Phone,
                                    EmailAddress = ad.EmailAddress,
                                    Mobile = ad.Mobile,
                                    IsActive = ad.IsActive,
                                    Paddress = ad.Paddress,
                                    ClocationId = ad.ClocationId,
                                    CdistrictId = ad.CdistrictId,
                                    Cvillage = ad.Cvillage,


                                    LocationText = resultLocation.Name,
                                    //AdressTypeText = string.Empty,
                                    DistrictText = resultDistrict.Name,
                                    ClocationText = resultCLocation.Name,
                                    CdistrictText = resultCdistrict.Name
                                     
                                }).ToListAsync(cancellationToken);
            } 
            return result;
        }
    }
}
