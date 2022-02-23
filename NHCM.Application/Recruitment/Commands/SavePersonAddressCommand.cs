using MediatR;
using Microsoft.EntityFrameworkCore;
using NHCM.Application.Common;
using NHCM.Application.Recruitment.Models;
using NHCM.Application.Recruitment.Queries;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using NHCM.Persistence.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NHCM.Application.Recruitment.Commands
{
    public class SavePersonAddressCommand : IRequest<List<SearchedPersonAdress>>
    {
        public decimal? Id { get; set; }
        public decimal PersonId { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public short? AddressTypeId { get; set; }
        public int? LocationId { get; set; }
        public int? DistrictId { get; set; }
        public string Village { get; set; }
        public string Address1 { get; set; }
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

    public class SavePersonAddressCommandHandler : IRequestHandler<SavePersonAddressCommand, List<SearchedPersonAdress>>
    {

        private HCMContext _context;
        private IMediator _mediator;
        private readonly ICurrentUser _currentUser;
        public SavePersonAddressCommandHandler(HCMContext context, IMediator mediator, ICurrentUser currentUser)
        {
            _context = context;
            _mediator = mediator;
            _currentUser = currentUser;
        }
        public async Task<List<SearchedPersonAdress>> Handle(SavePersonAddressCommand request, CancellationToken cancellationToken)
        {
            List<SearchedPersonAdress> result = new List<SearchedPersonAdress>();

            if(request.Id == null || request.Id == default(decimal))
            {
                int CurrentUserId = await _currentUser.GetUserId();

                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        using (_context)
                        {
                            Address personAddress = new Address()
                            {

                                PersonId = request.PersonId,
                                ModifiedOn = request.ModifiedOn,
                                ModifiedBy = request.ModifiedBy,
                                ReferenceNo = request.ReferenceNo,
                                CreatedOn = request.CreatedOn,
                                CreatedBy = request.CreatedBy,
                                AddressTypeId = request.AddressTypeId ?? 1, // Untill new Database schema
                                LocationId = request.LocationId,
                                DistrictId = request.DistrictId,
                                Village = request.Village,
                                Address1 = request.Address1,
                                StreetNo = request.StreetNo,
                                HouseNo = request.HouseNo,
                                Phone = request.Phone,
                                EmailAddress = request.EmailAddress,
                                Mobile = request.Mobile,
                                IsActive = request.IsActive,
                                Paddress = request.Paddress,
                                ClocationId = request.ClocationId,
                                CdistrictId = request.CdistrictId,
                                Cvillage = request.Cvillage,
                            };

                            _context.Address.Add(personAddress);
                            await _context.SaveChangesAsync(CurrentUserId, cancellationToken);


                            result = await _mediator.Send(new SearchPersonAddressQuery() { Id = personAddress.Id });

                            transaction.Commit();
                        }

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception();
                    } 
                } 
                
            } 
            else
            {

                using (_context)
                {
                    Address personAddressForUpdate = await _context.Address.Where(ad => ad.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

                    personAddressForUpdate.PersonId = request.PersonId;
                    personAddressForUpdate.ModifiedOn = request.ModifiedOn;
                    personAddressForUpdate.ModifiedBy = request.ModifiedBy;
                    personAddressForUpdate.ReferenceNo = request.ReferenceNo;
                    personAddressForUpdate.CreatedOn = request.CreatedOn ?? DateTime.Now;
                    personAddressForUpdate.CreatedBy = request.CreatedBy ?? 10; // Untill application of Identity
                    personAddressForUpdate.AddressTypeId = request.AddressTypeId ?? 1; // Untill new Database schema
                    personAddressForUpdate.LocationId = request.LocationId;
                    personAddressForUpdate.DistrictId = request.DistrictId;
                    personAddressForUpdate.Village = request.Village;
                    personAddressForUpdate.Address1 = request.Address1;
                    personAddressForUpdate.StreetNo = request.StreetNo;
                    personAddressForUpdate.HouseNo = request.HouseNo;
                    personAddressForUpdate.Phone = request.Phone;
                    personAddressForUpdate.EmailAddress = request.EmailAddress;
                    personAddressForUpdate.Mobile = request.Mobile;
                    personAddressForUpdate.IsActive = request.IsActive;
                    personAddressForUpdate.Paddress = request.Paddress;
                    personAddressForUpdate.ClocationId = request.ClocationId;
                    personAddressForUpdate.CdistrictId = request.CdistrictId;
                    personAddressForUpdate.Cvillage = request.Cvillage;

                   await _context.SaveChangesAsync(cancellationToken);

                    result = await _mediator.Send(new SearchPersonAddressQuery() { Id = personAddressForUpdate.Id });
                }
               
                
            }

            return result;
        }
    }
}
